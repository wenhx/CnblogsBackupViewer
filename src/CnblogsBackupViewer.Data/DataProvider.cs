using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnblogsBackupViewer.Data
{
    public abstract class DataProvider
    {
        public abstract void LoadData(string dataFilePath);
        public abstract bool IsFileSupported(string fileName);

        public abstract IList<Blog> Blogs { get; }

        public abstract Site Site { get; }
    }
}
