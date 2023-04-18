using CnblogsBackupViewer.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnblogsBackupViewer.Data
{
    public static class DataProviderFactory
    {
        static readonly List<DataProvider> _Providers = new List<DataProvider>()
        {
            new SQLiteDataProvider(),
        };

        public static DataProvider Create(string dataFilePath)
        {
            var provider = _Providers.FirstOrDefault(p => p.IsFileSupported(dataFilePath));

            if (provider == null)
                throw new Exception("文件类型不支持。");

            provider.LoadData(dataFilePath);
            return provider;
        }
    }
}
