namespace SBC_2D.Views
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPageDis = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelDis = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelDeviceConnections = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDos = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelDos = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageTest = new System.Windows.Forms.TabPage();
            this.tabPageDis.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageDos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageDis
            // 
            this.tabPageDis.Controls.Add(this.flowLayoutPanelDis);
            this.tabPageDis.Location = new System.Drawing.Point(4, 22);
            this.tabPageDis.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDis.Name = "tabPageDis";
            this.tabPageDis.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageDis.Size = new System.Drawing.Size(784, 481);
            this.tabPageDis.TabIndex = 0;
            this.tabPageDis.Text = "輸入";
            this.tabPageDis.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelDis
            // 
            this.flowLayoutPanelDis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelDis.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDis.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanelDis.Name = "flowLayoutPanelDis";
            this.flowLayoutPanelDis.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelDis.Size = new System.Drawing.Size(780, 477);
            this.flowLayoutPanelDis.TabIndex = 64;
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.Controls.Add(this.flowLayoutPanelDeviceConnections);
            this.tabPageSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetting.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageSetting.Size = new System.Drawing.Size(784, 481);
            this.tabPageSetting.TabIndex = 1;
            this.tabPageSetting.Text = "設定";
            this.tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelDeviceConnections
            // 
            this.flowLayoutPanelDeviceConnections.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDeviceConnections.Location = new System.Drawing.Point(7, 5);
            this.flowLayoutPanelDeviceConnections.Name = "flowLayoutPanelDeviceConnections";
            this.flowLayoutPanelDeviceConnections.Size = new System.Drawing.Size(771, 250);
            this.flowLayoutPanelDeviceConnections.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDis);
            this.tabControl1.Controls.Add(this.tabPageDos);
            this.tabControl1.Controls.Add(this.tabPageSetting);
            this.tabControl1.Controls.Add(this.tabPageTest);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 507);
            this.tabControl1.TabIndex = 63;
            // 
            // tabPageDos
            // 
            this.tabPageDos.Controls.Add(this.flowLayoutPanelDos);
            this.tabPageDos.Location = new System.Drawing.Point(4, 22);
            this.tabPageDos.Name = "tabPageDos";
            this.tabPageDos.Size = new System.Drawing.Size(784, 481);
            this.tabPageDos.TabIndex = 2;
            this.tabPageDos.Text = "輸出";
            this.tabPageDos.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelDos
            // 
            this.flowLayoutPanelDos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelDos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDos.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelDos.Name = "flowLayoutPanelDos";
            this.flowLayoutPanelDos.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelDos.Size = new System.Drawing.Size(784, 481);
            this.flowLayoutPanelDos.TabIndex = 65;
            // 
            // tabPageTest
            // 
            this.tabPageTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageTest.Name = "tabPageTest";
            this.tabPageTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTest.Size = new System.Drawing.Size(784, 481);
            this.tabPageTest.TabIndex = 3;
            this.tabPageTest.Text = "測試";
            this.tabPageTest.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 507);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.ShowIcon = false;
            this.Text = "Form3";
            this.tabPageDis.ResumeLayout(false);
            this.tabPageSetting.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPageDis;
        private System.Windows.Forms.TabPage tabPageSetting;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDeviceConnections;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDis;
        private System.Windows.Forms.TabPage tabPageDos;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDos;
        private System.Windows.Forms.TabPage tabPageTest;
    }
}