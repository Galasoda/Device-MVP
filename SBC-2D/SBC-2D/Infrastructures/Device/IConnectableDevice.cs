using SBC_2D.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public interface IConnectableDevice : IDevice
    {
        bool IsConnected { get; }
        event Action<string, bool> ConnectionChanged;
        bool Connect(IConnectionConfig config);
        void Disconnect();
        bool CheckConnection();
    }
}
