using SBC_2D.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SBC_2D.Presenters;
using SBC_2D.Views.Interfaces;
using System.Linq;

namespace SBC_2D.Views
{
    public partial class Form3 : Form, IForm3View
    {
        private readonly List<IDeviceConnectionView> _deviceConnectionViews;
        private readonly List<IIoView> _inputViews;
        private readonly List<IIoView> _outputViews;
        public IReadOnlyList<IDeviceConnectionView> DeviceConnectionViews { get => _deviceConnectionViews; }
        public IReadOnlyList<IIoView> InputViews { get => _inputViews; }
        public IReadOnlyList<IIoView> OutputViews { get => OutputViews; }

        public Form3()
        {
            InitializeComponent();
            _deviceConnectionViews = new List<IDeviceConnectionView>();
            _inputViews = new List<IIoView>();
            _outputViews = new List<IIoView>();
        }

        // Presenter 呼叫這個來建立 UI
        public IDeviceConnectionView AddDeviceConnectionView()
        {
            var control = new DeviceConnectionControl();
            flowLayoutPanelDeviceConnections.Controls.Add(control);
            _deviceConnectionViews.Add(control);
            return control;
        }

        public IIoView AddInputView(int number)
        {
            var control = new IoControl(false, number);
            flowLayoutPanelDis.Controls.Add(control);
            _inputViews.Add(control);
            return control;
        }

        public IOutView AddOutputView(int number)
        {
            var control = new IoControl(true, number);
            flowLayoutPanelDos.Controls.Add(control);
            _outputViews.Add(control);
            return control;
        }

        public void ClearInputView()
        {
            _inputViews.Clear();
        }

        public void ClearOutputView()
        {
            _outputViews.Clear();
        }
    }
}

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