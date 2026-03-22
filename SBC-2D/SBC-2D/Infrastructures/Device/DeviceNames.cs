using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Device
{
    public static class DeviceNames
    {
        public const string IoModule1 = "IoModule1";
        public const string IoModule2 = "IoModule2";
        public const string UpperBarcodeReader = "UpperBarcodeReader";
        public const string LowerBarcodeReader = "LowerBarcodeReader";
        public const string LaserDisplacementSensor = "LaserDisplacementSensor";

        public static readonly IReadOnlyList<string> List = new[]
        {
            IoModule1,
            IoModule2,
            UpperBarcodeReader,
            LowerBarcodeReader,
            LaserDisplacementSensor
        };
    }
}
