using SBC_2D.Infrastructures.Ini;
using SBC_2D.Shared;
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
    }
}
