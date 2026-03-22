using System.Windows.Forms;

namespace SBC_2D.Views.UserControls
{
    partial class IoControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label labelStatus;
        private Label labelNumber;
        private Label labelDescription;
        private FlowLayoutPanel flowLayout;

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
            this.flowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.flowLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayout
            // 
            this.flowLayout.Controls.Add(this.labelStatus);
            this.flowLayout.Controls.Add(this.labelNumber);
            this.flowLayout.Controls.Add(this.labelDescription);
            this.flowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayout.Location = new System.Drawing.Point(0, 0);
            this.flowLayout.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayout.Name = "flowLayout";
            this.flowLayout.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayout.Size = new System.Drawing.Size(186, 30);
            this.flowLayout.TabIndex = 0;
            this.flowLayout.WrapContents = false;
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.Gray;
            this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatus.Location = new System.Drawing.Point(3, 3);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(1);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(25, 25);
            this.labelStatus.TabIndex = 0;
            // 
            // labelNumber
            // 
            this.labelNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNumber.Location = new System.Drawing.Point(30, 3);
            this.labelNumber.Margin = new System.Windows.Forms.Padding(1);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(60, 25);
            this.labelNumber.TabIndex = 1;
            this.labelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDescription
            // 
            this.labelDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDescription.Location = new System.Drawing.Point(92, 3);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(1);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(92, 25);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayout);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "IoControl";
            this.Size = new System.Drawing.Size(186, 30);
            this.flowLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }

    #endregion
}
