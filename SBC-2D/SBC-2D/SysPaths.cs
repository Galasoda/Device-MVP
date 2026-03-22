using SBC_2D.Infrastructures;
using System.IO;
using System.Windows.Forms;

namespace SBC_2D
{
    class SysPaths
    {
        public static string BaseDir { get; private set; }
        public static string SetupIni { get; private set; }
        public static string BskDir { get; private set; }
        public static string XmlDir { get; set; }
        public static string LangDir { get; private set; }
        public static string LogDir { get; private set; }
        public static string ErrorLogDir { get; private set; }
        public static string MyLogDir { get; private set; }
        public static string BarcodeLogDir { get; private set; }
        public static string LockState { get; private set; }
        public static string ModelDir { get; private set; }
        public static string TempModelPath { get; private set; }
        public static string InsertType { get; set; }
        public static string SqliteFile { get; private set; }

        public static void Init()
        {
            BaseDir = Application.StartupPath;
            SetupIni = BaseDir + @"\Setup\Setup.ini";
            string bskDir = IniFile.ReadIniFile("Path", "BskDir", "", 1024, SetupIni, "UTF-8");
            BskDir = BaseDir + $@"{bskDir}";
            string xmlDir = IniFile.ReadIniFile("Path", "XmlDir", "", 1024, SetupIni, "UTF-8");
            XmlDir = xmlDir;
            string langDir = IniFile.ReadIniFile("Path", "LangDir", "", 1024, SetupIni, "UTF-8");
            LangDir = BaseDir + $@"{langDir}";
            string logDir = IniFile.ReadIniFile("Path", "LogDir", "", 1024, SetupIni, "UTF-8");
            LogDir = BaseDir + $@"{logDir}";
            string errorLogDir = IniFile.ReadIniFile("Path", "ErrorLogDir", "", 1024, SetupIni, "UTF-8");
            ErrorLogDir = BaseDir + $@"{errorLogDir}";
            string barcodeLogDir = IniFile.ReadIniFile("Path", "BarcodeLogDir", "", 1024, SetupIni, "UTF-8");
            BarcodeLogDir = BaseDir + $@"{barcodeLogDir}";
            string myLogDir = IniFile.ReadIniFile("Path", "MyLogDir", "", 1024, SetupIni, "UTF-8");
            MyLogDir = BaseDir + $@"{myLogDir}";
            string lockState = IniFile.ReadIniFile("Path", "LockState", "", 1024, SetupIni, "UTF-8");
            LockState = BaseDir + $@"{lockState}";
            string modelDir = IniFile.ReadIniFile("Path", "ModelDir", "", 1024, SetupIni, "UTF-8");
            ModelDir = BaseDir + $@"{modelDir}";
            TempModelPath = Path.Combine(ModelDir, "Temp", "Param.ini");
            InsertType = IniFile.ReadIniFile("Path", "InsertType", "", 1024, SetupIni, "UTF-8");
            SqliteFile = Path.Combine(BaseDir, "Database", "ASE_A3_SBC-2D.db");
        }
    }
}
