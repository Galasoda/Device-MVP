using Advantech.Adam;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SBC_2D.Domain.Servicies
{
    public class IniService
    {
        private readonly IniStore _store;

        public IniService(IniStore store)
        {
            _store = store;
        }

        public Setup GetSetup()
        {
            var pathConfig = GetPathConfig();
            var deviceConfig = GetDeviceConfig();
            var productionConfig = GetProductionConfig();
            _store.Setup = new Setup(deviceConfig, productionConfig, pathConfig);
            return _store.Setup;
        }

        public PathConfig GetPathConfig()
        {
            PathConfig path = new PathConfig();
            string basePath = Application.StartupPath;
            string bugInfo = string.Empty;
            string section = "Path";

            if (!IniFile.TryGetValue(section, "InsertType", "", _store.SetupFilePath, out string insertType))
                bugInfo += "Missing InsertType.\r\n";
            else
                path.InsertType = basePath + insertType;

            if (!IniFile.TryGetValue(section, "BskDir", "", _store.SetupFilePath, out string bskDir))
                bugInfo += "Missing BskDir.\r\n";
            else
                path.BskDir = basePath + bskDir;

            if (!IniFile.TryGetValue(section, "XmlDir", "", _store.SetupFilePath, out string xmlDir))
                bugInfo += "Missing XmlDir.\r\n";
            else
                path.XmlDir = basePath + xmlDir;

            if (!IniFile.TryGetValue(section, "LockState", "", _store.SetupFilePath, out string lockState))
                bugInfo += "Missing LockState.\r\n";
            else
                path.LockState = basePath + lockState;

            if (!IniFile.TryGetValue(section, "SqLiteFile", "", _store.SetupFilePath, out string sqLiteFile))
                bugInfo += "Missing SqLiteFile.\r\n";
            else
                path.SqLiteFile = basePath + sqLiteFile;

            if (!string.IsNullOrEmpty(bugInfo))
            {
            }

            return path;
        }

        public DeviceConfig GetDeviceConfig()
        {
            DeviceConfig deviceConfig = new DeviceConfig();
            try
            {
                Dictionary<string, SocketConfig> socketConfigs = new Dictionary<string, SocketConfig>();
                IEnumerable<string> deviceNames = DeviceNames.List;
                foreach (string section in deviceNames)
                {
                    string bugInfo = "";
                    if (!IniFile.TryGetValue(section, "IP", "", _store.SetupFilePath, out string ip))
                        bugInfo += "Missing IP.\r\n";
                    if (!IniFile.TryGetValue(section, "PORT", "", _store.SetupFilePath, out string port_))
                        bugInfo += "Missing PORT.\r\n";
                    if (!int.TryParse(port_, out int port))
                    {
                        bugInfo += "Invalid PORT.\r\n";
                        port = -1;
                    }
                    if (!string.IsNullOrEmpty(bugInfo))
                    {
                        continue;
                    }
                    SocketConfig config = new SocketConfig(ip, port);
                    socketConfigs.Add(section, config);
                }
                deviceConfig.SocketConfigs = socketConfigs;
            }
            catch (Exception ex)
            {
            }

            return deviceConfig;
        }

        public KeyValuePair<string, SocketConfig> GetSocketConfig(string section)
        {
            KeyValuePair<string, SocketConfig> result = new KeyValuePair<string, SocketConfig>(section, null);
            string bugInfo = "";
            try
            {
                IEnumerable<string> deviceNames = DeviceNames.List;
                if (!deviceNames.Contains(section))
                    return result;
                if (!IniFile.TryGetValue(section, "IP", "", _store.SetupFilePath, out string ip))
                    bugInfo += "Missing IP.\r\n";
                if (!IniFile.TryGetValue(section, "PORT", "", _store.SetupFilePath, out string port_))
                    bugInfo += "Missing PORT.\r\n";
                if (!int.TryParse(port_, out int port))
                {
                    bugInfo += "Invalid PORT.\r\n";
                    port = -1;
                }
                if (!string.IsNullOrEmpty(bugInfo))
                {
                    return result;
                }
                result = new KeyValuePair<string, SocketConfig>(section, new SocketConfig(ip, port)); 
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        private ProductionConfig GetProductionConfig()
        {
            ProductionConfig config = new ProductionConfig();
            string section = "Production";
            string bugInfo = string.Empty;

            if (!IniFile.TryGetValue(section, "Pqty", "", _store.SetupFilePath, out string pqtyStr) || !int.TryParse(pqtyStr, out int pqty))
                bugInfo += "Invalid or missing Pqty.\r\n";
            else
                config.Pqty = pqty;

            if (!IniFile.TryGetValue(section, "ModelName", "", _store.SetupFilePath, out string modelName))
                bugInfo += "Missing ModelName.\r\n";
            else
                config.ModelName = modelName;

            if (!IniFile.TryGetValue(section, "Delay_BoardStopAck", "", _store.SetupFilePath, out string delayBoardStopAckStr) ||
                !long.TryParse(delayBoardStopAckStr, out long delayBoardStopAck))
                bugInfo += "Invalid or missing Delay_BoardStopAck.\r\n";
            else
                config.Delay_BoardStopAck = delayBoardStopAck;

            if (!IniFile.TryGetValue(section, "Timeout_WaitStopper", "", _store.SetupFilePath, out string timeoutWaitStopperStr) ||
                !long.TryParse(timeoutWaitStopperStr, out long timeoutWaitStopper))
                bugInfo += "Invalid or missing Timeout_WaitStopper.\r\n";
            else
                config.Timeout_WaitStopper = timeoutWaitStopper;

            if (!IniFile.TryGetValue(section, "Timeout_WaitInPlace", "", _store.SetupFilePath, out string timeoutWaitInPlaceStr) ||
                !long.TryParse(timeoutWaitInPlaceStr, out long timeoutWaitInPlace))
                bugInfo += "Invalid or missing Timeout_WaitInPlace.\r\n";
            else
                config.Timeout_WaitInPlace = timeoutWaitInPlace;

            if (!IniFile.TryGetValue(section, "Timeout_WaitExit", "", _store.SetupFilePath, out string timeoutWaitExitStr) ||
                !long.TryParse(timeoutWaitExitStr, out long timeoutWaitExit))
                bugInfo += "Invalid or missing Timeout_WaitExit.\r\n";
            else
                config.Timeout_WaitExit = timeoutWaitExit;

            if (!IniFile.TryGetValue(section, "Timeout_OnEntry", "", _store.SetupFilePath, out string timeoutOnEntryStr) ||
                !long.TryParse(timeoutOnEntryStr, out long timeoutOnEntry))
                bugInfo += "Invalid or missing Timeout_OnEntry.\r\n";
            else
                config.Timeout_OnEntry = timeoutOnEntry;

            if (!IniFile.TryGetValue(section, "Timeout_OnExit", "", _store.SetupFilePath, out string timeoutOnExitStr) ||
                !long.TryParse(timeoutOnExitStr, out long timeoutOnExit))
                bugInfo += "Invalid or missing Timeout_OnExit.\r\n";
            else
                config.Timeout_OnExit = timeoutOnExit;

            if (!IniFile.TryGetValue(section, "Timeout_MeasuThickness", "", _store.SetupFilePath, out string timeoutMeasuThicknessStr) ||
                !long.TryParse(timeoutMeasuThicknessStr, out long timeoutMeasuThickness))
                bugInfo += "Invalid or missing Timeout_MeasuThickness.\r\n";
            else
                config.Timeout_MeasuThickness = timeoutMeasuThickness;

            if (!IniFile.TryGetValue(section, "Timeout_ReadBarcode", "", _store.SetupFilePath, out string timeoutReadBarcodeStr) ||
                !long.TryParse(timeoutReadBarcodeStr, out long timeoutReadBarcode))
                bugInfo += "Invalid or missing Timeout_ReadBarcode.\r\n";
            else
                config.Timeout_ReadBarcode = timeoutReadBarcode;

            if (!IniFile.TryGetValue(section, "Timeout_SendBsk", "", _store.SetupFilePath, out string timeoutSendBskStr) ||
                !long.TryParse(timeoutSendBskStr, out long timeoutSendBsk))
                bugInfo += "Invalid or missing Timeout_SendBsk.\r\n";
            else
                config.Timeout_SendBsk = timeoutSendBsk;

            if (!string.IsNullOrEmpty(bugInfo))
            {
            }

            return config;
        }

        public void SaveSetupSectoinIpPort(string section, string ip, string port)
        {
            string keyIp = "IP";
            IniFile.Write(section, keyIp, ip, _store.SetupFilePath);
            string keyPort = "PORT";
            IniFile.Write(section, keyPort, port, _store.SetupFilePath);
        }

        /* Helper From Model Layer */
        public bool TryGetSocketConfig(string name, out SocketConfig config)
            => _store.TryGetSocketConfig(name, out config);
        public bool UpdateSetupSocketConfig(string name, SocketConfig config)
        {
            if (!_store.Setup.DeviceConfig.SocketConfigs.TryGetValue(name, out SocketConfig cfg))
                return false;
            cfg = config;
            return true;
        }

    }
}
