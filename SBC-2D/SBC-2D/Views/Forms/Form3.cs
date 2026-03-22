using SBC_2D.ViewModels;
using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SBC_2D.Shared;
using SBC_2D.Presenters;
using SBC_2D.Views.Interfaces;
using System.Linq;

namespace SBC_2D.Views
{
    public partial class Form3 : Form, IForm3View
    {
        private DevicePresenter _devicePresenter;
        public List<IDeviceConnectionView> DeviceConnectionViews { get; }
        public List<IIoView> InputViews { get; }
        public List<IIoView> OutputViews { get; }

        public Form3()
        {
            InitializeComponent();
            DeviceConnectionViews = new List<IDeviceConnectionView>();
            InputViews = new List<IIoView>();
            OutputViews = new List<IIoView>();
        }

        public void SetPresenter(DevicePresenter presenter)
        {
            _devicePresenter = presenter;
        }

        // Presenter 呼叫這個來建立 UI
        public IDeviceConnectionView AddDeviceConnectionView()
        {
            var control = new DeviceConnectionControl();
            flowLayoutPanelDeviceConnections.Controls.Add(control);
            DeviceConnectionViews.Add(control);
            return control;
        }

        public IIoView AddInputView(int number)
        {
            var control = new IoControl(false, number);
            flowLayoutPanelDis.Controls.Add(control);
            InputViews.Add(control);
            return control;
        }

        public IOutView AddOutputView(int number)
        {
            var control = new IoControl(true, number);
            flowLayoutPanelDos.Controls.Add(control);
            OutputViews.Add(control);
            return control;
        }
    }
}


/* Device list for user setting */
//public void BindDeviceListVm(DeviceConnectionListVm vm)
//{
//    _deviceConnectionControls = new Dictionary<DeviceName, DeviceConnectionControl>();
//    flowLayoutPanelDeviceConnections.Controls.Clear();
//    foreach (DeviceConnectionVm rowVm in vm.RowVms)
//    {
//        int yPosition = 300;
//        DeviceConnectionControl control = new DeviceConnectionControl();
//        control.Location = new Point(10, yPosition);
//        flowLayoutPanelDeviceConnections.Controls.Add(control);
//        DeviceName name = rowVm.DeviceName;
//        control.BindVm(rowVm);
//        control.ConnectClicked += (s, e) => OnConnectClick(name, e);
//        control.IpChanged += (s, ip) => OnIpChanged(name, ip);
//        control.PortChanged += (s, port) => OnPortChanged(name, port);
//        _deviceConnectionControls[name] = control;
//        yPosition += control.Height + 5;
//    }
//}

///* System i/o */
//public void BindSystemIoListVm(SystemIoListVm vm)
//{
//    _disControls = new Dictionary<string, IoLayout>();
//    flowLayoutPanelDis.Controls.Clear();
//    foreach (SystemIoVm diVm in vm.DiVms)
//    {
//        IoLayout control = new IoLayout(false);
//        flowLayoutPanelDis.Controls.Add(control);
//        control.BindVm(diVm);
//        string id = $"{diVm.Prefix}{diVm.Number}";
//        _disControls[id] = control;
//    }
//    _dosControls = new Dictionary<string, IoLayout>();
//    flowLayoutPanelDos.Controls.Clear();
//    foreach (SystemIoVm doVm in vm.DoVms)
//    {
//        IoLayout control = new IoLayout(true);
//        flowLayoutPanelDos.Controls.Add(control);
//        control.BindVm(doVm);
//        control.OutputClicked += Control_OutputClicked;
//        string id = $"{doVm.Prefix}{doVm.Number}";
//        _dosControls[id] = control;
//    }
//}

//protected override void OnResizeBegin(EventArgs e)
//{
//    base.OnResizeBegin(e);
//    SuspendLayout();
//}

//protected override void OnResizeEnd(EventArgs e)
//{
//    base.OnResizeEnd(e);
//    ResumeLayout();
//}