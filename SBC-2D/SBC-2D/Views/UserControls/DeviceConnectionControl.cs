using SBC_2D.Infrastructures.Ini;
using SBC_2D.Presenters;
using SBC_2D.Views.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SBC_2D.Views.UserControls
{
    public partial class DeviceConnectionControl : UserControl, IDeviceConnectionView
    {
        public event EventHandler<EndPointArgs> RequestConnection;
        public event EventHandler<string> IpChanged;
        public event EventHandler<string> PortChanged;

        public DeviceConnectionControl()
        {
            InitializeComponent();
            textBoxIP.TextChanged += (s, e) => IpChanged?.Invoke(this, textBoxIP.Text);
            textBoxPort.TextChanged += (s, e) => PortChanged?.Invoke(this, textBoxPort.Text);
            buttonConnect.Click += (s, e) => RequestConnection?.Invoke(this,
                new EndPointArgs(textBoxIP.Text, int.TryParse(textBoxPort.Text, out int p) ? p : -1));
        }
        public void SetName(string name)
        {
            SafeInvoke(() =>
            {
                labelDevice.Text = name;
                int lbWidth = labelDevice.Width;
                int left = labelDevice.Left;
                Width = left + lbWidth;
                
                //labelDevice.Width = txtWidth;
                //if (Width < left + lbWidth)
                //    Width = left + lbWidth;
                //else

            });
        }
        public void SetIp(string ip) => SafeInvoke(() => { if (textBoxIP.Text != ip) textBoxIP.Text = ip; });
        public void SetPort(string port) => SafeInvoke(() => { if (textBoxPort.Text != port) textBoxPort.Text = port; });
        public void SetConnecting(bool value) => SafeInvoke(() => { buttonConnect.Text = value ? "連線中..." : "連線"; buttonConnect.Enabled = !value; });
        public void SetConnected(bool value) => SafeInvoke(() => panelStatus.BackColor = value ? Color.Green : Color.Gray);

        private void SafeInvoke(Action action)
        {
            if (IsDisposed || Disposing) return;
            if (InvokeRequired) Invoke(action);
            else action();
        }
    }
    public class EndPointArgs : EventArgs
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public EndPointArgs(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }
    }
}
