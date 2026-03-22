using System;
using System.Collections.Generic;
using System.Linq;
using SBC_2D.Shared;
using SBC_2D.Infrastructures.Ini;

namespace SBC_2D.Infrastructures.Device
{
    public class DevicesStore
    {
        public List<IDevice> Devices { get; set; }
        public List<IoDeviceContext> IoDeviceContext { get; set; }

        public DevicesStore()
        {
            Devices = new List<IDevice>();
            IoDeviceContext = new List<IoDeviceContext>();
        }

        public bool TryGetConnectableDevice(string name, out IConnectableDevice device)
        {
            device = Devices.OfType<IConnectableDevice>().FirstOrDefault(d => d.Name.Equals(name));
            return device != null;
        }


        public bool TryGet<T>(string name, out T device) where T : class
        {
            device = null;
            foreach (IDevice d in Devices)
            {
                if (d is IDevice && d.Name == name)
                {
                    device = d as T;
                    return device != null;
                }
            }
            return false;
        }

        public bool AddDevice(IDevice device)
        {
            if (device == null)
                return false;

            if (Devices.OfType<IDevice>().Any(x => x.Name == device.Name))
                return false;

            Devices.Add(device);
            return true;
        }
    }
}
