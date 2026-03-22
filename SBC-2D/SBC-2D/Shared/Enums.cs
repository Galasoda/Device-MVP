using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Shared
{
    public enum LoggerName
    {
        Error,
        System,
        CodeTrace,
        Barcode
    }

    public enum LogType
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Critical
    }
}
