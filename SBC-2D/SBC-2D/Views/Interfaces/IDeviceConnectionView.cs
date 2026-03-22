using SBC_2D.Infrastructures.Ini;
using SBC_2D.Presenters;
using SBC_2D.Shared;
using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Views.Interfaces
{
    public interface IDeviceConnectionView
    {
        event EventHandler<EndPointArgs> RequestConnection;
        event EventHandler<string> IpChanged;
        event EventHandler<string> PortChanged;
        void SetName(string name);
        void SetIp(string ip);
        void SetPort(string port);
        void SetConnecting(bool isConnecting);
        void SetConnected(bool isConnected);
    }
}
