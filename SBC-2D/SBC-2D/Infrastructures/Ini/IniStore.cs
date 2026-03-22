using IniParser.Model;
using System.Windows.Forms;

namespace SBC_2D.Infrastructures.Ini
{
    public class IniStore
    {
        public string SetupFilePath { get; } = Application.StartupPath + @"\Setup\Setup.ini";
        public Setup Setup { get; set; }
        public bool TryGetSocketConfig(string name, out SocketConfig config)
            => Setup.DeviceConfig.SocketConfigs.TryGetValue(name, out config);
    }
}