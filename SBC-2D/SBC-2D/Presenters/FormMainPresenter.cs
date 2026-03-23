using SBC_2D.Views;
using SBC_2D.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Presenters
{
    public class FormMainPresenter
    {
        private readonly IFormMainView _view;
        private readonly DevicePresenter _devicePresenter;

        public FormMainPresenter(IFormMainView view, DevicePresenter devicePresenter)
        {
            _view = view;                        // ← 補上賦值
            _devicePresenter = devicePresenter;
        }

        public void Initialize()
        {
            _view.Loaded += OnLoaded;
            _view.PageRequested += OnPageRequested;
        }

        private async void OnLoaded(object sender, EventArgs e)
        {
            _devicePresenter.Initialize();
            await _devicePresenter.ConnectAllAsync();
            _devicePresenter.StartPollingAllDeviceConnection();
            _devicePresenter.StartUpdatingAllDeviceDio();
        }

        private void OnPageRequested(object sender, string pageName)
        {
            if (!IsValidPage(pageName))
            {
                return;
            }
            _view.NavigateTo(pageName);
        }

        private static bool IsValidPage(string pageName) =>
            pageName == PageNames.Form1 ||
            pageName == PageNames.Form2 ||
            pageName == PageNames.Form3 ||
            pageName == PageNames.Form4;

        private async void FormMainView_Loading(object sender, EventArgs e)
        {
            //_devicePresenter.Initialize();
            //await _devicePresenter.ConnectAll();
            //_devicePresenter.StartPollingAllDeviceConnection();
            //_devicePresenter.StartUpdatingAllDeviceDio();
        }
    }
}
