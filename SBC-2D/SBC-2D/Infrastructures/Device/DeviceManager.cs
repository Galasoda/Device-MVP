using SBC_2D.Infrastructures.Ini;
using SBC_2D.Infrastructures.Logger;
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
        private IniStore _iniStore;
        private DevicesStore _deviceStore;
        private SemaphoreSlim _connectLimit;
        private SemaphoreSlim _checkStatusLimit;
        private Task _updateDiosTask;
        private Task _updateStatusTask;
        private CancellationTokenSource _ctsKeepUpdateStatus;
        private CancellationTokenSource _ctsKeepUpdateDios;
        public bool IsStartedUpdatingStatus { get => _ctsKeepUpdateStatus != null && !_ctsKeepUpdateStatus.IsCancellationRequested; }
        public bool IsStartedUpdatingDios { get => _ctsKeepUpdateDios != null && !_ctsKeepUpdateDios.IsCancellationRequested; }

        public DeviceManager(DevicesStore devicesStore, IniStore iniStore)
        {
            _deviceStore = devicesStore;
            _connectLimit = new SemaphoreSlim(3);
            _checkStatusLimit = new SemaphoreSlim(5);
            _iniStore = iniStore;
        }

        public IReadOnlyList<IDevice> CreateDevices()
        {
            List<IDevice> devices = new List<IDevice>();
            foreach (var config in _iniStore.Setup.DeviceConfig.SocketConfigs)
            {
                bool isExist = DeviceFactory.BuildMapping.TryGetValue(config.Key, out Func<IDevice> build);
                if (!isExist)
                    continue;
                IDevice device = build();
                devices.Add(device);
            }
            _deviceStore.Devices.Clear();
            _deviceStore.Devices = devices;
            return devices;
        }

        public List<IoDeviceContext> CreateIoDeviceContexts()
        {
            List<IoDeviceContext> ioDeviceContext = new List<IoDeviceContext>();
            IEnumerable<IIoDevice> ioDevices = _deviceStore.Devices.OfType<IIoDevice>();

            int systemDiIndex = 0;
            int systemDoIndex = 0;
            foreach (IIoDevice device in ioDevices)
            {
                IoState ioState = new IoState(device.DiCount, device.DoCount);
                IoDeviceContext ioInstance = new IoDeviceContext(
                    device,
                    systemDiIndex,
                    systemDoIndex,
                    ioState
                );
                systemDiIndex += device.DiCount;
                systemDoIndex += device.DoCount;
                ioDeviceContext.Add(ioInstance);
            }
            _deviceStore.IoDeviceContext.Clear();
            _deviceStore.IoDeviceContext = ioDeviceContext;
            //Log: $"Created {_deviceStore.Devices.Count} {nameof(IoDeviceContext)}."
            return ioDeviceContext;
        }

        /* Connection */
        public async Task<int> ConnectAllAsync()
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (var device in _deviceStore.Devices.OfType<IConnectableDevice>())
            {
                tasks.Add(ConnectAsync(device));
            }
            bool[] results = await Task.WhenAll(tasks);
            //Log: Connected {count} out of {_deviceStore.Devices.count} devices
            return results.Count(b => b);
        }

        public async Task<bool> ConnectAsync(IConnectableDevice device)
        {
            _iniStore.TryGetSocketConfig(device.Name, out var config);
            if (config == null)
                return false;

            await _connectLimit.WaitAsync();
            try
            {
                return await Task.Run(() => device.Connect(config));
            }
            catch(Exception ex)
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
            {
                LoggerStore.RecordCodeTrace(LogType.Info, $"Device {name} can't be able connect, because it's not found.");
                return false;
            }
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
                                    LoggerStore.RecordCodeTrace(LogType.Warn, $"Device {device.Name} CheckConnection failed: {ex.Message}");
                                }
                            }));

                        await Task.WhenAll(tasks);

                        await Task.Delay(1000, _ctsKeepUpdateStatus.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    LoggerStore.RecordSystem(LogType.Info, "Stopped checking connection.");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Stopped checking connection.");
                }
            });
            return _updateStatusTask;
        }

        public async Task StopUpdatingConnectionStatus()
        {
            if (_ctsKeepUpdateStatus == null)
            {
                LoggerStore.RecordSystem(LogType.Debug, "StopUpdatingConnectionStatus skipped: CTS is null.");
                return;
            }

            LoggerStore.RecordSystem(LogType.Info, "Stopping connection status update task...");
            _ctsKeepUpdateStatus.Cancel();

            try
            {
                if (_updateStatusTask != null)
                {
                    await _updateStatusTask;
                }

                LoggerStore.RecordSystem(LogType.Info, "Connection status update task stopped successfully.");
            }
            catch (TaskCanceledException)
            {
                LoggerStore.RecordSystem(LogType.Info, "Connection status update task was canceled.");
            }
            catch (Exception ex)
            {
                LoggerStore.RecordSystem(LogType.Error, $"Error while stopping connection status task: {ex}");
            }
            finally
            {
                _ctsKeepUpdateStatus.Dispose();
                _ctsKeepUpdateStatus = null;
                _updateStatusTask = null;
                LoggerStore.RecordCodeTrace(LogType.Debug, "StopUpdatingConnectionStatus cleanup completed.");
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
                                LoggerStore.RecordCodeTrace(LogType.Warn, $"Device {ctx.Device.Name} CheckConnection failed: {ex.Message}");
                            }
                        }));

                        await Task.WhenAll(tasks);
                        await Task.Delay(100, _ctsKeepUpdateDios.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    LoggerStore.RecordSystem(LogType.Info, "DIO update stopped.");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"DIO update stopped");
                }
                catch (Exception ex)
                {
                    LoggerStore.RecordSystem(LogType.Error, "DIO update failed.");
                    LoggerStore.RecordCodeTrace(LogType.Debug, $"Exception: {ex.Message}");
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