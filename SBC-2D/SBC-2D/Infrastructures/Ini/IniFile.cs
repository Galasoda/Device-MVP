using IniParser;
using IniParser.Model;
using SBC_2D.Shared;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SBC_2D.Infrastructures.Ini
{
    public static class IniFile
    {
        private static readonly FileIniDataParser _parser = new FileIniDataParser();
        private static readonly object _lock = new object();

        static IniFile()
        {
            _parser.Parser.Configuration.AllowDuplicateSections = false;
            _parser.Parser.Configuration.CaseInsensitive = true;
        }

        private static IniData Load(string filePath)
        {
            if (!File.Exists(filePath))
                return new IniData();
            return _parser.ReadFile(filePath, Encoding.UTF8);
        }

        private static void Save(IniData data, string filePath)
        {
            File.Copy(filePath, filePath + ".bak", overwrite: true);
            _parser.WriteFile(filePath, data, new UTF8Encoding(false));
        }

        public static List<string> GetAllSectionNames(string filePath)
        {
            var data = Load(filePath);
            return data.Sections.Select(s => s.SectionName).ToList();
        }

        public static void Write(string section, string key, string value, string filePath)
        {
            lock (_lock)
            {
                var data = Load(filePath);

                if (!data.Sections.ContainsSection(section))
                    data.Sections.AddSection(section);

                data[section][key] = value ?? string.Empty;

                Save(data, filePath);
            }
        }

        public static string Read(string section, string key, string defaultValue, string filePath)
        {
            lock (_lock)
            {
                var data = Load(filePath);

                if (!data.Sections.ContainsSection(section))
                    return defaultValue;

                return data[section][key] ?? defaultValue;
            }
        }

        public static void DeleteKey(string section, string key, string filePath)
        {
            lock (_lock)
            {
                var data = Load(filePath);

                if (data.Sections.ContainsSection(section))
                    data[section].RemoveKey(key);

                Save(data, filePath);
            }
        }

        public static void DeleteSection(string section, string filePath)
        {
            lock (_lock)
            {
                var data = Load(filePath);
                data.Sections.RemoveSection(section);
                Save(data, filePath);
            }
        }

        public static bool TryGetValue(string section, string key, string defaultValue, string filePath, out string value)
        {
            value = IniFile.Read(section, key, defaultValue, filePath);
            if (value == null || value == defaultValue)
            {
                return false;
            }
            return true;
        }
    }
}
