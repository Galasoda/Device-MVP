using SBC_2D.Infrastructures;
using SBC_2D.Presenters;
using SBC_2D.Views.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SBC_2D.Views
{
    public partial class FormMain : Form, IFormMainView
    {
        private Form3 _form3;
        public event EventHandler Loaded;
        public event EventHandler<string> PageRequested;

        public FormMain(Form3 f3)
        {
            InitializeComponent();
            ApplyTheme();
            _form3 = f3;
        }

        private void ApplyTheme()
        {
            AppTheme.ApplyForm(this);
            AppTheme.ApplyContant(panelPage);
            AppTheme.ApplyTopbar(panelTopbar);
            AppTheme.ApplyBottombar(panelBottombar);
        }

        private void FormMain_Load(object sender, EventArgs e)
            => Loaded?.Invoke(this, EventArgs.Empty);

        private void ButtonNavigate_Click(object sender, EventArgs e)
        {

        }
            => PageRequested?.Invoke(this, (sender as Button)?.Name ?? "");

        public void NavigateTo(string pageName)
        {
            // 切換頁面邏輯，例如顯示對應的 Panel 或 Form
            switch (pageName)
            {
                case PageNames.Form3: ShowPage(_form3); break;
            }
        }

        private void ShowPage(Form page)
        {
            if (page == null)
                return;
            panelPage.Controls.Clear();
            page.TopLevel = false;
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;
            panelPage.Controls.Add(page);
            page.Show();
            page.Focus();
        }

        public void SetModelName(string modelName)
            => labelModelName.Text = modelName;
        public void SetVersion(string version)
            => labelVersion.Text = version;
        public void SetMachineStatus(string status)
            => labelStatus.Text = status;
        public void SetUserRole(string role)
            => labelUserRole.Text = role;
    }
}


//private Form _form3;
//public event EventHandler Loaded;
//public event EventHandler<string> PageRequested;

//public FormMain(Form3 f3)
//{
//    _form3 = f3;
//}

//private void FormMain_Load(object sender, EventArgs e)
//    => Loaded?.Invoke(this, EventArgs.Empty);

//private void ButtonPage_Click(object sender, EventArgs e)
//    => PageRequested?.Invoke(this, (sender as Button)?.Name ?? "");

//public void NavigateTo(string pageName)
//{
//    // 切換頁面邏輯，例如顯示對應的 Panel 或 Form
//    switch (pageName)
//    {
//        case PageNames.Form3: ShowPage(_form3); break;
//    }
//}

//private void ShowPage(Form page)
//{
//    if (page == null)
//        return;
//    panelPage.Controls.Clear();
//    page.TopLevel = false;
//    page.FormBorderStyle = FormBorderStyle.None;
//    page.Dock = DockStyle.Fill;
//    panelPage.Controls.Add(page);
//    page.Show();
//    page.Focus();
//}

//public void SetModelName(string modelName)
//    => labelModelName.Text = modelName;
//public void SetVersion(string version)
//    => labelVersion.Text = version;
//public void SetMachineStatus(string status)
//    => labelStatus.Text = status;
//public void SetUserRole(string role)
//    => labelUserRole.Text = role;