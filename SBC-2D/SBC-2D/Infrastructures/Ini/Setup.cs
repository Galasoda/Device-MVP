using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace SBC_2D.Infrastructures.Ini
{
    public class DeviceConfig
    {
        public Dictionary<string, SocketConfig> SocketConfigs { get; set; }
        public DeviceConfig()
        {
            SocketConfigs = new Dictionary<string, SocketConfig>();
        }
        public DeviceConfig DeepClone()
        {
            return new DeviceConfig
            {
                SocketConfigs = SocketConfigs
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.DeepClone()
                    )
            };
        }
    }

    public class PathConfig
    {
        public string InsertType { get; set; }
        public string BskDir { get; set; }
        public string XmlDir { get; set; }
        public string LockState { get; set; }
        public string SqLiteFile { get; set; }
    }

    public class ProductionConfig
    {
        public string ModelName { get; set; }
        public int Pqty { get; set; }
        public long Delay_BoardStopAck { get; set; }
        public long Timeout_WaitStopper { get; set; }
        public long Timeout_WaitInPlace { get; set; }
        public long Timeout_WaitExit { get; set; }
        public long Timeout_OnEntry { get; set; }
        public long Timeout_OnExit { get; set; }
        public long Timeout_MeasuThickness { get; set; }
        public long Timeout_ReadBarcode { get; set; }
        public long Timeout_SendBsk { get; set; }
    }

    public class Setup
    {
        public DeviceConfig DeviceConfig { get; }
        public ProductionConfig ProductionConfig { get; }
        public PathConfig PathConfig { get; }

        public Setup(DeviceConfig deviceConfig,
                     ProductionConfig productionConfig,
                     PathConfig pathConfig)
        {
            DeviceConfig = deviceConfig;
            ProductionConfig = productionConfig;
            PathConfig = pathConfig;
        }
    }
}
