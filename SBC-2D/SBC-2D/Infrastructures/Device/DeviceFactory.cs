using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.VisualStyles;

namespace SBC_2D.Infrastructures.Device
{
    public static class DeviceFactory
    {
        public static readonly Dictionary<string, Func<IDevice>> BuildMapping;

        static DeviceFactory()
        {
            BuildMapping = new Dictionary<string, Func<IDevice>>()
            {
                { DeviceNames.IoModule1, () => new Adam6052(DeviceNames.IoModule1) },
                { DeviceNames.IoModule2, () => new Adam6052(DeviceNames.IoModule2) },
                { DeviceNames.UpperBarcodeReader, () => new KeyenceBarcodeReader(DeviceNames.UpperBarcodeReader) },
                { DeviceNames.LowerBarcodeReader, () => new KeyenceBarcodeReader(DeviceNames.LowerBarcodeReader) },
                { DeviceNames.LaserDisplacementSensor, () => new KeyenceBarcodeReader(DeviceNames.LaserDisplacementSensor) },
            };
        }

        public static List<IDevice> BuildDevices()
        {
            return BuildMapping.Select(kv => kv.Value()).ToList();
        }

        public static List<IDevice> CreateDevices(DeviceConfig deviceConfig)
        {
            List<IDevice> devices = new List<IDevice>();
            foreach (var config in deviceConfig.SocketConfigs)
            {
                bool isExist = BuildMapping.TryGetValue(config.Key, out Func<IDevice> build);
                if (!isExist)
                    continue;
                IDevice device = build();
                devices.Add(device);
            }
            return devices;
        }

        public static List<IoDeviceContext> CreateIoDeviceContexts(IEnumerable<IIoDevice> ioDevices)
        {
            List<IoDeviceContext> ioDeviceContext = new List<IoDeviceContext>();
            int systemDiIndex = 0;
            int systemDoIndex = 0;
            foreach (IIoDevice device in ioDevices)
            {
                IoState ioState = new IoState(device.DiCount, device.DoCount);
                IoDeviceContext ioInstance = new IoDeviceContext(
                    device,
                    systemDiIndex,
                    systemDoIndex,
                    ioState
                );
                systemDiIndex += device.DiCount;
                systemDoIndex += device.DoCount;
                ioDeviceContext.Add(ioInstance);
            }
            return ioDeviceContext;
        }
    }
}
