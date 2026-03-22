using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Logger
{
    public class FileLogger
    {
        private static readonly object _lock = new object();
        private readonly string _baseDir;

        public FileLogger(string baseDir)
        {
            _baseDir = baseDir;
        }

        public void Record(string message)
        {
            var date = DateTime.Now.ToString("yyyyMMdd");
            var dir = Path.Combine(_baseDir, date);
            var path = Path.Combine(dir, $"{date}.txt");

            Directory.CreateDirectory(dir);

            try
            {
                lock (_lock)
                {
                    File.AppendAllText(
                        path,
                        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}{Environment.NewLine}",
                        Encoding.UTF8
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
