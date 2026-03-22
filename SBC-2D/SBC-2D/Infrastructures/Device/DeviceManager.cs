using SBC_2D.Infrastructures.Ini;
using SBC_2D.Shared;
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
        private DevicesStore _deviceStore;
        private DeviceConfig _deviceConfig;
        private SemaphoreSlim _connectLimit;
        private SemaphoreSlim _checkStatusLimit;
        private Task _updateDiosTask;
        private Task _updateStatusTask;
        private CancellationTokenSource _ctsKeepUpdateStatus;
        private CancellationTokenSource _ctsKeepUpdateDios;
        public bool IsStartedUpdatingStatus { get => _ctsKeepUpdateStatus != null && !_ctsKeepUpdateStatus.IsCancellationRequested; }
        public bool IsStartedUpdatingDios { get => _ctsKeepUpdateDios != null && !_ctsKeepUpdateDios.IsCancellationRequested; }

        public DeviceManager(DevicesStore devicesStore, DeviceConfig deviceConfig)
        {
            _deviceStore = devicesStore;
            _deviceConfig = deviceConfig;
            _connectLimit = new SemaphoreSlim(3);
            _checkStatusLimit = new SemaphoreSlim(5);
        }

        /* Connection */
        public async Task<int> ConnectAllAsync()
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            var devices = _deviceStore.Devices.OfType<IConnectableDevice>();
            foreach (var device in devices)
            {
                string name = device.Name;
                if (!_deviceConfig.SocketConfigs.TryGetValue(name, out var config))
                    continue;
                tasks.Add(ConnectAsync(device, config));
            }
            bool[] results = await Task.WhenAll(tasks);
            //Log: Connected {count} out of {_deviceStore.Devices.count} devices
            return results.Count(b => b);
        }

        public async Task<bool> ConnectAsync(IConnectableDevice device, IConnectionConfig config)
        {
            await _connectLimit.WaitAsync();
            try
            {
                return await Task.Run(() => device.Connect(config));
            }
            catch (Exception ex)
            {
                //LOG
                return false;
            }
            finally
            {
                _connectLimit.Release();
            }
        }

        public async Task<bool> CheckConnection(string name)
        {
            if (!_deviceStore.TryGetConnectableDevice(name, out IConnectableDevice device))
                return false;
            return device.CheckConnection();
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
                        var tasks = _deviceStore.Devices
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
            }
            finally
            {
                _ctsKeepUpdateStatus.Dispose();
                _ctsKeepUpdateStatus = null;
                _updateStatusTask = null;
            }
        }

        public Task StartUpdatingDios()
        {
            _ctsKeepUpdateDios = new CancellationTokenSource();
            _updateDiosTask = Task.Run(async () =>
            {
                try
                {
                    while (!_ctsKeepUpdateDios.Token.IsCancellationRequested)
                    {
                        var tasks = _deviceStore.IoDeviceContext
                        .Select(ctx => Task.Run(() =>
                        {
                            try
                            {
                                ctx.UpdateDis();
                                ctx.UpdateDos();
                            }
                            catch (Exception ex)
                            {
                            }
                        }));

                        await Task.WhenAll(tasks);
                        await Task.Delay(100, _ctsKeepUpdateDios.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                }
            });

            return _updateDiosTask;
        }

        public async Task StopUpdatingDios()
        {
            if (_ctsKeepUpdateDios == null)
                return;

            _ctsKeepUpdateDios.Cancel();

            try
            {
                if (_ctsKeepUpdateDios != null)
                    await _updateDiosTask;
            }
            catch (TaskCanceledException)
            {
                // 忽略，代表正常停止
            }
            finally
            {
                _ctsKeepUpdateDios.Dispose();
                _ctsKeepUpdateDios = null;
                _updateDiosTask = null;
            }
        }

        public bool ControlDo(int systemIndex, bool isOn)
        {
            int index = -1;
            foreach (var iodc in _deviceStore.IoDeviceContext)
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
            foreach (var iodc in _deviceStore.IoDeviceContext)
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