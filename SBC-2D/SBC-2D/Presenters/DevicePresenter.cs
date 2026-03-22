using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Shared;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Presenters
{
    public class DevicePresenter
    {
        private readonly IForm3View _form3View;
        private readonly DeviceService _deviceService;
        private readonly IniService _iniService;
        private readonly List<DeviceConnectionPresenter> _deviceConnectionPresenters;
        private readonly List<IoDeviceContext> _ioDeviceContexts;
        private Dictionary<int, IIoView> _diViewMap;
        private Dictionary<int, IIoView> _doViewMap;

        public DevicePresenter(IForm3View form3View, DeviceService deviceService, IniService iniService)
        {
            _form3View = form3View;
            _deviceService = deviceService;
            _deviceConnectionPresenters = new List<DeviceConnectionPresenter>();
            _ioDeviceContexts = new List<IoDeviceContext>();
            _diViewMap = new Dictionary<int, IIoView>();
            _doViewMap = new Dictionary<int, IIoView>();
            _iniService = iniService;
        }

        public void Initialize()
        {
            _deviceService.CreateDevices();
            _deviceService.CreateIoDeviceContexts();
            foreach (IDevice device in _deviceService.GetDevices())
            {
                string name = device.Name;
                var view = _form3View.AddDeviceConnectionView();
                var config = _iniService.GetSocketConfig(name);
                if (config.Value == default)
                    config = new KeyValuePair<string, SocketConfig>(name, new SocketConfig("", -1));
                var presenter = new DeviceConnectionPresenter(view, _deviceService, config.Key, config.Value);
                _deviceConnectionPresenters.Add(presenter);
                presenter.Initialize();
            }
            foreach (IoDeviceContext context in _deviceService.GetIoDeviceContexts())
            {
                _ioDeviceContexts.Add(context);
                for (int i = 0; i < context.Device.DiCount; i++)
                {
                    int systemDiNumber = context.ToSystemDi(i);
                    IIoView view = _form3View.AddInputView(systemDiNumber);
                    view.SetNumber(systemDiNumber);
                    view.SetDescription($"{"X"}{systemDiNumber}用ini設定");
                    view.SetStatus(context.State.Dis[i]);
                    _diViewMap.Add(systemDiNumber, view);
                }
                context.SystemDisUpdated -= Context_SystemDisUpdated;
                context.SystemDisUpdated += Context_SystemDisUpdated;
                for (int i = 0; i < context.Device.DoCount; i++)
                {
                    int systemDoNumber = context.ToSystemDo(i);
                    IOutView view = _form3View.AddOutputView(systemDoNumber);
                    view.SetNumber(systemDoNumber);
                    view.SetDescription($"{"Y"}{systemDoNumber}用ini設定");
                    view.SetStatus(context.State.Dos[i]);
                    _doViewMap.Add(systemDoNumber, view);
                    view.OutputClicked += View_OutputClicked; ;
                }
                context.SystemDosUpdated -= Context_SystemDosUpdated;
                context.SystemDosUpdated += Context_SystemDosUpdated;
            }
        }

        private void View_OutputClicked(object sender, int e)
        {
            _deviceService.InverseDo(e);
        }

        //不宣告查表: 時間換空間
        //宣告查表: 空間換時間
        private void Context_SystemDisUpdated(IReadOnlyDictionary<int, bool> dis)
        {
            foreach (var din in dis)
            {
                if (_diViewMap.TryGetValue(din.Key, out IIoView view))
                    view.SetStatus(din.Value);
            }
        }

        private void Context_SystemDosUpdated(IReadOnlyDictionary<int, bool> dos)
        {
            foreach (var dout in dos)
            {
                if (_doViewMap.TryGetValue(dout.Key, out IIoView view))
                    view.SetStatus(dout.Value);
            }
        }

        public async Task ConnectAll()
        {
            foreach (var p in _deviceConnectionPresenters)
                await p.TriggerConnectAsync();
        }

        public void StartPollingAllDeviceConnection()
        {
            _deviceService.StartPollingAllDeviceConnection();
        }

        public void StartUpdatingAllDeviceDio()
        {
            _deviceService.StartUpdatingAllDeviceDio();
        }

    }
}
