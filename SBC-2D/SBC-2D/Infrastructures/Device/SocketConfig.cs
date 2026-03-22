using SBC_2D.Infrastructures.Device;
using SBC_2D.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Ini
{
    public class SocketConfig : IConnectionConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public SocketConfig(string address, int port)
        {
            Address = address;
            Port = port;
        }

        public SocketConfig DeepClone()
        {
            return new SocketConfig(Address, Port);
        }
    }
}
