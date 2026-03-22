using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
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
        private readonly IniService _iniService;
        private readonly DeviceManager _manager;
        private readonly DevicesStore _store;
        private readonly DeviceConfig _deviceConfig;

        public DeviceService(DeviceManager manager, DevicesStore store, IniService iniService)
        {
            _store = store;
            _manager = manager;
            _iniService = iniService;
            _deviceConfig = _iniService.GetDeviceConfig();
        }

        public IReadOnlyList<IDevice> CreateDevices()
        {
            List<IDevice> devices = new List<IDevice>();
            foreach (var config in _deviceConfig.SocketConfigs)
            {
                bool isExist = DeviceFactory.BuildMapping.TryGetValue(config.Key, out Func<IDevice> build);
                if (!isExist)
                    continue;
                IDevice device = build();
                devices.Add(device);
            }
            _store.Devices.Clear();
            _store.Devices.AddRange(devices);
            return devices;
        }

        public List<IoDeviceContext> CreateIoDeviceContexts()
        {
            List<IoDeviceContext> ioDeviceContext = new List<IoDeviceContext>();
            IEnumerable<IIoDevice> ioDevices = _store.Devices.OfType<IIoDevice>();

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
            _store.IoDeviceContext.Clear();
            _store.IoDeviceContext.AddRange(ioDeviceContext);
            //Log: $"Created {_deviceStore.Devices.Count} {nameof(IoDeviceContext)}."
            return ioDeviceContext;
        }

        public async Task<int> ConnectAllAsync() 
            => await _manager.ConnectAllAsync();

        public async Task<bool> ConnectAsync(string name, SocketConfig config)
        {
            if(!_store.TryGetConnectableDevice(name, out var device))
                return false;
            if(!_deviceConfig.SocketConfigs.TryGetValue(name, out var cfg))
                return false;
            cfg = config;
            bool isConnected = await _manager.ConnectAsync(device, cfg);
            if (isConnected)
                _iniService.SaveSetupSectoinIpPort(name, cfg.Address, cfg.Port.ToString());
            return isConnected;
        }

        public void StartPollingAllDeviceConnection()
        {
            if (_manager.IsStartedUpdatingStatus)
            {
                return;
            }
            Task task = _manager.StartPollingAllDeviceConnection();
        }

        public void StartUpdatingAllDeviceDio()
        {
            if (_manager.IsStartedUpdatingDios)
            {
                return;
            }
            Task task = _manager.StartUpdatingDios();
        }

        public void InverseDo(int systemIndex)
        {
            bool isInversed = _manager.InverseDo(systemIndex, out bool isOn);
            string triedMsg = isInversed ? "success" : "failed";
            string statusMsg = isOn ? "on" : "off";
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
