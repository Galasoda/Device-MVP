using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SBC_2D.Infrastructures.Device
{
    public class DeviceManager
    {
        private IReadOnlyList<IDevice> _devices;
        private IReadOnlyList<IConnectableDevice> _connectableDevices;
        private IReadOnlyList<IoDeviceContext> _ioDeviceContexts;
        private DeviceConfig _deviceConfig;
        private SemaphoreSlim _connectLimit;
        private SemaphoreSlim _checkStatusLimit;
        private Task _updateStatusTask;
        private CancellationTokenSource _ctsKeepUpdateStatus;
        public bool IsStartedUpdatingStatus { get => _ctsKeepUpdateStatus != null && !_ctsKeepUpdateStatus.IsCancellationRequested; }

        public DeviceManager()
        {
            _connectLimit = new SemaphoreSlim(3);
            _checkStatusLimit = new SemaphoreSlim(5);
            _deviceConfig = new DeviceConfig();
        }

        public void Initialize(DevicesStore devicesStore, DeviceConfig deviceConfig)
        {
            _devices = devicesStore.Devices;
            _connectableDevices = _devices.OfType<IConnectableDevice>().ToList();
            foreach (var d in _connectableDevices)
            {
                d.ConnectionChanged -= Device_ConnectionChanged;
                d.ConnectionChanged += Device_ConnectionChanged;
            }
            _ioDeviceContexts = devicesStore.IoDeviceContext;
            _deviceConfig = deviceConfig;
        }

        private async void Device_ConnectionChanged(string name, bool status)
        {
            var iodc = _ioDeviceContexts.FirstOrDefault(c => c.Device.Name == name);
            if (status)
            {
                if (!iodc.IsStartedUpdatingDios)
                    _ = iodc.StartUpdatingDios();
            }
            else
            {
                if (iodc.IsStartedUpdatingDios)
                    await iodc.StopUpdatingDios();
            }
        }

        /* Connection */
        public async Task<Dictionary<string, bool>> ConnectAllAsync()
        {
            var tasks = _devices
                .OfType<IConnectableDevice>()
                .Where(d => _deviceConfig.SocketConfigs.ContainsKey(d.Name))
                .Select(d => ConnectAsync(d, _deviceConfig.SocketConfigs[d.Name]));

            (string Name, bool Value)[] results = await Task.WhenAll(tasks);

            return results.ToDictionary(r => r.Name, r => r.Value);
        }

        public async Task<(string Name, bool Value)> ConnectAsync(
            IConnectableDevice device,
            IConnectionConfig config)
        {
            await _connectLimit.WaitAsync();
            try
            {
                bool isConnected = await Task.Run(() => device.Connect(config));
                return (device.Name, isConnected);
            }
            catch (Exception ex)
            {
                return (device.Name, false);
            }
            finally
            {
                _connectLimit.Release();
            }
        }

        /* Polling */
        public Task StartPollingAllDeviceConnection()
        {
            _ctsKeepUpdateStatus = new CancellationTokenSource();
            _updateStatusTask = Task.Run(async () =>
            {
                try
                {
                    while (!_ctsKeepUpdateStatus.Token.IsCancellationRequested)
                    {
                        var tasks = _devices
                            .OfType<IConnectableDevice>()
                            .Select(device => Task.Run(() =>
                            {
                                try
                                {
                                    device.CheckConnection();
                                }
                                catch (Exception ex)
                                {
                                }
                            }));

                        await Task.WhenAll(tasks);
                        await Task.Delay(1000, _ctsKeepUpdateStatus.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    //出問題要waring
                }
            });
            return _updateStatusTask;
        }

        public async Task StopUpdatingConnectionStatus()
        {
            if (_ctsKeepUpdateStatus == null)
            {
                return;
            }

            _ctsKeepUpdateStatus.Cancel();

            try
            {
                if (_updateStatusTask != null)
                {
                    await _updateStatusTask;
                }

            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                //出問題要waring
            }
            finally
            {
                _ctsKeepUpdateStatus.Dispose();
                _ctsKeepUpdateStatus = null;
                _updateStatusTask = null;
            }
        }

        public async Task StartUpdatingAllDios()
        {
            var tasks = new List<Task>();
            foreach (var iodc in _ioDeviceContexts)
                tasks.Add(iodc.StartUpdatingDios());
            await Task.WhenAll(tasks);
        }

        public async Task StopUpdatingAllDios()
        {
            var tasks = new List<Task>();
            foreach (var iodc in _ioDeviceContexts)
                tasks.Add(iodc.StopUpdatingDios());
            await Task.WhenAll(tasks);
        }

        public bool ControlDo(int systemIndex, bool isOn)
        {
            int index = -1;
            foreach (var iodc in _ioDeviceContexts)
            {
                if (iodc.TryToDeviceDo(systemIndex, out index))
                {
                    iodc.Device.WriteDo(index, isOn);
                    return true;
                }
            }
            return false;
        }

        public bool InverseDo(int systemIndex, out bool isOn)
        {
            isOn = false;
            bool isInversed = false;
            foreach (var iodc in _ioDeviceContexts)
            {
                if (iodc.TryToDeviceDo(systemIndex, out int index))
                {
                    isInversed = iodc.Device.InverseDo(index, out bool result);
                    isOn = result;
                    break;
                }
            }
            return isInversed;
        }
    }
}