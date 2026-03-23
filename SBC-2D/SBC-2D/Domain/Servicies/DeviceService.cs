using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
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
        private readonly IniService _iniService;
        private readonly DeviceManager _manager;
        private readonly DevicesStore _store;
        private DeviceConfig _deviceConfig;

        public DeviceService(DeviceManager manager, DevicesStore store, IniService iniService)
        {
            _store = store;
            _manager = manager;
            _iniService = iniService;
        }

        public IReadOnlyList<IDevice> CreateDevices()
        {
            List<IDevice> devices = new List<IDevice>();
            _deviceConfig = _iniService.GetDeviceConfig();
            devices = DeviceFactory.CreateDevices(_deviceConfig);
            _store.Devices.Clear();
            _store.Devices.AddRange(devices);
            return devices;
        }

        public List<IoDeviceContext> CreateIoDeviceContexts()
        {
            IEnumerable<IIoDevice> ioDevices = _store.Devices.OfType<IIoDevice>();
            List<IoDeviceContext> ioDeviceContext = DeviceFactory.CreateIoDeviceContexts(ioDevices);
            _store.IoDeviceContext.Clear();
            _store.IoDeviceContext.AddRange(ioDeviceContext);
            return ioDeviceContext;
        }

        public async Task ConnectAllAsync()
        {
            await _manager.ConnectAllAsync();
        }

        public async Task<bool> ConnectAsync(string name, SocketConfig config)
        {
            if (!_store.TryGetConnectableDevice(name, out var device))
                return false;
            if (!_deviceConfig.SocketConfigs.TryGetValue(name, out var cfg))
                return false;
            cfg = config;
            var result = await _manager.ConnectAsync(device, cfg);
            if (result.Value)
                _iniService.SaveSetupSectoinIpPort(name, cfg.Address, cfg.Port.ToString());
            return result.Value;
        }

        public async Task StartPollingAllDeviceConnection()
        {
            if (_manager.IsStartedUpdatingStatus)
                return;
            await _manager.StartPollingAllDeviceConnection();
        }

        public async Task StartUpdatingAllDeviceDio()
        {
            await _manager.StartUpdatingAllDios();
        }

        public void InverseDo(int systemIndex)
        {
            bool isInversed = _manager.InverseDo(systemIndex, out bool isOn);
        }


        /* Helper Model Layer*/
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
        //public KeyValuePair<string, SocketConfig> GetSocketConfig(string name)
        //    => _iniService.GetSocketConfig(name);
    }
}
