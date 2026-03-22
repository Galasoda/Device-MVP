using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public class DeviceIdentity
    {
        string Name { get; }
        string Model { get; }

        public DeviceIdentity(string name, string model)
        {
            Name = name;
            Model = model;
        }
    }
}
