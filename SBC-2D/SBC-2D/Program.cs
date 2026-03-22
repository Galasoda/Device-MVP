using Dapper.FluentMap;
using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Presenters;
using SBC_2D.ViewModels;
using SBC_2D.Views;
using System;
using System.Windows.Forms;

namespace SBC_2D
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 

        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IniStore iniStore = new IniStore();
            IniService iniService = new IniService(iniStore);
            iniService.GetSetup();

            DevicesStore devicesStore = new DevicesStore();
            DeviceManager deviceManager = new DeviceManager(devicesStore, iniStore);
            DeviceService deviceService = new DeviceService(deviceManager, devicesStore, iniService);

            Form3 form3 = new Form3();
            FormMain formMain = new FormMain(form3);
            DevicePresenter devicePresenter = new DevicePresenter(form3, deviceService, iniService);
            formMain.SetPresenters(devicePresenter);

            Application.Run(formMain);
        }
    }
}
