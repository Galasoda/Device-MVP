using SBC_2D.Infrastructures;
using SBC_2D.Presenters;
using SBC_2D.ViewModels;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SBC_2D.Views
{
    public partial class FormMain : Form
    {
        private Form3 _form3;
        private DevicePresenter _devicePresenter;
        private Form _currentPage;

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

        public void SetPresenters(DevicePresenter devicePresenter)
        {
            _devicePresenter = devicePresenter;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            _devicePresenter.Initialize();
            await _devicePresenter.ConnectAll();
            _devicePresenter.StartPollingAllDeviceConnection();
            _devicePresenter.StartUpdatingAllDeviceDio();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {

        }

        /* Switch Page */
        public void SwitchPage(Form page)
        {
            if (page == null)
                return;
            page.TopLevel = false;
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;
            if (_currentPage != null)
                panelPage.Controls.Remove(_currentPage);
            _currentPage = page;
            panelPage.Controls.Add(page);
            page.Show();
            page.Focus();
        }

        private void ButtonSwitchPage_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            switch (button)
            {
                case var r when r == buttonForm3:
                    SwitchPage(_form3);
                    break;
            }
        }
    }
}
