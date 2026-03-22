using SBC_2D.ViewModels;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBC_2D.Views.UserControls
{
    public partial class IoControl : UserControl, IIoView, IOutView
    {
        public int SystemIndex { get; }
        private string prefix = "";
        public event EventHandler<int> OutputClicked;

        public IoControl(bool isOutput, int index)
        {
            InitializeComponent();
            SystemIndex = index;
            prefix = isOutput ? "Y" : "X";
            if (isOutput)
                labelStatus.Click += LabelStatus_Click;
            else
                labelStatus.Click -= LabelStatus_Click;
        }

        public void SetStatus(bool isOn)
            => SafeInvoke(() => labelStatus.BackColor = isOn ? Color.LimeGreen : Color.Gray);

        public void SetNumber(int number)
            => SafeInvoke(() =>
            {
                labelNumber.Text = $"{prefix}{number}";
                AdjustWidth(labelNumber);
            });

        public void SetDescription(string description)
            => SafeInvoke(() =>
            {
                labelDescription.Text = description;
                AdjustWidth(labelDescription);
            });

        private void LabelStatus_Click(object sender, EventArgs e)
            => OutputClicked?.Invoke(this, SystemIndex);

        private void AdjustWidth(Label label)
        {
            label.AutoSize = true;
            int lbWidth = label.Width;
            label.AutoSize = false;
            int txtWidth = TextRenderer.MeasureText(label.Text, label.Font).Width;
            label.Width = txtWidth + (lbWidth - txtWidth);
        }

        private void SafeInvoke(Action action)
        {
            if (IsDisposed || Disposing) return;
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}
