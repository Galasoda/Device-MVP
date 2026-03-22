using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Infrastructures.Logger;
using SBC_2D.Shared;
using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;


namespace SBC_2D.Domain.Servicies
{
    public class DeviceService
    {
        private readonly DeviceManager _manager;
        private readonly DevicesStore _store;
        private readonly IniService _iniService;

        public DeviceService(DeviceManager manager, DevicesStore store, IniService iniService)
        {
            _store = store;
            _manager = manager;
            _iniService = iniService;
        }

        public async Task<int> ConnectAllAsync() 
            => await _manager.ConnectAllAsync();

        public async Task<bool> ConnectAsync(string name)
        {
            if(!_store.TryGetConnectableDevice(name, out IConnectableDevice device))
                return false;
            return await _manager.ConnectAsync(device);
        }

        public void StartPollingAllDeviceConnection()
        {
            if (_manager.IsStartedUpdatingStatus)
            {
                LoggerStore.RecordSystem(LogType.Info, $"It's already start polling all devices connection.");
                return;
            }
            Task task = _manager.StartPollingAllDeviceConnection();
            if (task.Exception == null)
                LoggerStore.RecordSystem(LogType.Info, $"Started polling all devices connection.");
        }

        public void StartUpdatingAllDeviceDio()
        {
            if (_manager.IsStartedUpdatingDios)
            {
                LoggerStore.RecordSystem(LogType.Info, $"It's already start updating all device dio.");
                return;
            }
            Task task = _manager.StartUpdatingDios();
            if (task.Exception == null)
                LoggerStore.RecordSystem(LogType.Info, $"Started updating all device dio.");
        }

        public void InverseDo(int systemIndex)
        {
            bool isInversed = _manager.InverseDo(systemIndex, out bool isOn);
            string triedMsg = isInversed ? "success" : "failed";
            string statusMsg = isOn ? "on" : "off";
            LoggerStore.RecordSystem(LogType.Info, $"User control output {systemIndex} {triedMsg}. Now output is {statusMsg}");
        }


        /* Helper Model Layer*/
        public IReadOnlyList<IoDeviceContext> CreateIoDeviceContexts()
            => _manager.CreateIoDeviceContexts();
        public IReadOnlyList<IDevice> CreateDevices()
            => _manager.CreateDevices();
        public IReadOnlyList<IDevice> GetDevices()
            => _store.Devices.AsReadOnly();
        public IDevice GetDevice(string name)
            => _store.Devices.FirstOrDefault(d => d.Name == name);
        public IConnectableDevice GetConnectableDevice(string name)
            => _store.TryGetConnectableDevice(name, out var device) ? device : null;
        public IReadOnlyList<string> GetDeviceNames()
            => _store.Devices.Select(d => d.Name).ToList().AsReadOnly();
        public IReadOnlyList<IoDeviceContext> GetIoDeviceContexts()
            => _store.IoDeviceContext.AsReadOnly();
    }
}
