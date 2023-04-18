using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CnblogsBackupViewer
{
    internal class Constants
    {
        public static readonly string AppName = "Cnblogs Backup Viewer";
        public static readonly string AppVersion = GetAppVersion();

        private static string GetAppVersion()
        {
            var version = Assembly.GetEntryAssembly()!.GetName().Version!;
            return $"{version.Major}.{version.Minor}.{version.Revision}";
        }
    }
}
