using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using SBC_2D.Shared;

namespace SBC_2D.Infrastructures.Logger
{
    public static class LoggerStore
    {
        private static readonly Dictionary<LoggerName, FileLogger> _loggers = new Dictionary<LoggerName, FileLogger>()
        {
            [LoggerName.Error] = new FileLogger(Application.StartupPath + @"\Log\Error"),
            [LoggerName.System] = new FileLogger(Application.StartupPath + @"\Log\System"),
            [LoggerName.CodeTrace] = new FileLogger(Application.StartupPath + @"\Log\CodeTrace"),
            [LoggerName.Barcode] = new FileLogger(Application.StartupPath + @"\Log\Barcode")
        };

        public static event Action<LoggerName, string> LogRecorded;

        private static void Record(LoggerName logger, string message)
        {
            _loggers[logger].Record(message);
            LogRecorded?.Invoke(logger, message);
        }

        public static void RecordSystem(LogType type, string message)
        {
            string prefix = $"[{type.ToString()}]";
            string final = $"{prefix} {message}";
            Record(LoggerName.System, final);
        }

        public static void RecordBarcode(string message)
        {
            Record(LoggerName.Barcode, message);
        }

        public static void RecordCodeTrace(
            LogType type,
            string message,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "")
        {
            string prefix = $"[{type.ToString()}]";
            string final = $"{prefix} {message} => {Path.GetFileNameWithoutExtension(filePath)}.{memberName}";
            Record(LoggerName.CodeTrace, final);
        }

        public static void RecordError(string message)
        {
            Record(LoggerName.Error, message);
        }
    }
}
