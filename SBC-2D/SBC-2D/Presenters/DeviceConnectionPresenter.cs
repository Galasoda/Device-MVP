using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Shared;
using SBC_2D.Views.Interfaces;
using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SBC_2D.Presenters
{
    public class DeviceConnectionPresenter
    {
        private readonly IDeviceConnectionView _view;
        private readonly DeviceService _deviceService;
        private readonly IniService _iniService;
        private IConnectableDevice _device;
        private string _name = string.Empty;
        private SocketConfig _socketConfig;

        public DeviceConnectionPresenter(IDeviceConnectionView view, DeviceService deviceService, IniService iniService, string name)
        {
            if (deviceService == null)
                throw new ArgumentNullException(nameof(deviceService));
            if (view == null)
                throw new ArgumentNullException(nameof(view));
            _view = view;
            _deviceService = deviceService;
            _iniService = iniService;
            _name = name;
        }

        public void Initialize()
        {
            _view.IpChanged += View_IpChanged;
            _view.PortChanged += View_PortChanged;
            _view.RequestConnection += View_RquestedConnection;
            _socketConfig = _iniService.GetSocketConfig(_name).Value;
            _view.SetName(_name);
            _view.SetIp(_socketConfig.Address);
            _view.SetPort(_socketConfig.Port > -1 ? _socketConfig.Port.ToString() : "");
            _device = _deviceService.GetConnectableDevice(_name);
            if (_device != null)
                _device.ConnectionChanged += Device_ConnectionChanged;
        }

        private void View_IpChanged(object sender, string ip)
        {
            _socketConfig.Address = ip;
            _view.SetIp(ip);
        }

        private void View_PortChanged(object sender, string port)
        {
            if (!int.TryParse(port, out int p))
                return;
            _socketConfig.Port = p;
            _view.SetPort(port);
        }

        private async void View_RquestedConnection(object sender, EndPointArgs e)
        {
            bool isConnected = await TriggerConnectAsync();
            if (isConnected)
                _iniService.SaveSetupSectoinIpPort(_name,
                    _socketConfig.Address,
                    _socketConfig.Port.ToString());
        }

        private void Device_ConnectionChanged(string name, bool isConnected)
        {
            if (_device == null || name != _name)
                return;
            _view.SetConnected(isConnected);
        }

        public async Task<bool> TriggerConnectAsync()
        {
            _view.SetConnecting(true);
            bool isConnected = await _deviceService.ConnectAsync(_name);
            _view.SetConnecting(false);
            _view.SetConnected(isConnected);
            return isConnected;
        }
    }
}
