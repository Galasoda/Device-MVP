using System.Drawing;
using System.Windows.Forms;

namespace SBC_2D.Views.UserControls
{
    partial class DeviceConnectionControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TextBox textBoxIP;
        private TextBox textBoxPort;
        private Button buttonConnect;
        private Panel panelStatus;
        private Label labelDevice;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.labelDevice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(10, 10);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(150, 22);
            this.textBoxIP.TabIndex = 0;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(170, 10);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(60, 22);
            this.textBoxPort.TabIndex = 1;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(240, 9);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(60, 25);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "連線";
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.Gray;
            this.panelStatus.Location = new System.Drawing.Point(310, 9);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(25, 25);
            this.panelStatus.TabIndex = 3;
            // 
            // labelDevice
            // 
            this.labelDevice.Location = new System.Drawing.Point(345, 9);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(150, 25);
            this.labelDevice.TabIndex = 4;
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DeviceConnectionControl
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.labelDevice);
            this.Name = "DeviceConnectionControl";
            this.Size = new System.Drawing.Size(407, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
