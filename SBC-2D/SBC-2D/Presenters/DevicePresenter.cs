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
                var view = _form3View.AddDeviceConnectionView();
                var presenter = new DeviceConnectionPresenter(view, _deviceService, _iniService, device.Name);
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
            foreach (var v in _form3View.DeviceConnectionViews)
                v.SetConnecting(true);
            await _deviceService.ConnectAllAsync();
            foreach (var v in _form3View.DeviceConnectionViews)
                v.SetConnecting(false);
        }

        public void StartPollingAllDeviceConnection()
        {
            _deviceService.StartPollingAllDeviceConnection();
        }

        public void StartUpdatingAllDeviceDio()
        {
            _deviceService.StartUpdatingAllDeviceDio();
        }

        //public async Task<int> ConnectAll()
        //{
        //    int numConnected = 0;
        //    List<Task> tasks = new List<Task>();
        //    foreach (var p in _deviceConnectionPresenters)
        //    {
        //        tasks.Add(p.TriggConnectAsync());
        //    }
        //    await Task.WhenAll(tasks);
        //    return numConnected;
        //}
    }
}

//public void Initialize(IEnumerable<IoInstance> instances)
//{
//    _diVms.Clear();
//    _doVms.Clear();
//    foreach (IoInstance instance in instances)
//    {
//        for (int i = 0; i < instance.Device.DiCount; i++)
//        {
//            SystemIoVm diVm = new SystemIoVm()
//            {
//                IsOn = instance.State.Dis[i],
//                Prefix = "X",
//                Number = instance.DiMap[i],
//                Description = $"{"X"}{instance.DiMap[i]}用ini設定"
//            };
//            _diVms.Add(diVm);
//        }
//        for (int i = 0; i < instance.Device.DoCount; i++)
//        {
//            SystemIoVm doVm = new SystemIoVm()
//            {
//                IsOn = instance.State.Dos[i],
//                Prefix = "Y",
//                Number = instance.DoMap[i],
//                Description = $"{"Y"}{instance.DoMap[i]}用ini設定"
//            };
//            _doVms.Add(doVm);
//        }
//    }
//    OnPropertyChanged(nameof(DiVms));
//    OnPropertyChanged(nameof(DoVms));
//}

//public SystemIoVm GetDiVm(int number)
//{
//    return _diVms.FirstOrDefault(vm => vm.Number == number);
//}

//public SystemIoVm GetDoVm(int number)
//{
//    return _doVms.FirstOrDefault(vm => vm.Number == number);
//}

//public void UpdateDiStatus(Dictionary<int, bool> status)
//{
//    foreach (KeyValuePair<int, bool> kvp in status)
//    {
//        SystemIoVm diVm = GetDiVm(kvp.Key);
//        if (diVm != null)
//        {
//            diVm.IsOn = kvp.Value;
//        }
//    }
//}

//public void UpdateDoStatus(Dictionary<int, bool> status)
//{
//    foreach (KeyValuePair<int, bool> kvp in status)
//    {
//        SystemIoVm doVm = GetDoVm(kvp.Key);
//        if (doVm != null)
//        {
//            doVm.IsOn = kvp.Value;
//        }
//    }
//}
