using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnblogsBackupViewer.Data.SQLite
{
    internal class SQLiteDataProvider : DataProvider
    {
        BlogDbContext m_DbContext;
        IList<Blog> m_BlogList;

        public override IList<Blog> Blogs
        {
            get
            {
                return m_BlogList;
            }
        }

        public override bool IsFileSupported(string fileName)
        {
            return fileName.EndsWith(".db", StringComparison.OrdinalIgnoreCase);
        }

        public override void LoadData(string dataFilePath)
        {
            m_DbContext?.Dispose();
            m_DbContext = new BlogDbContext(dataFilePath);
            m_BlogList = m_DbContext.Blogs.OrderByDescending(b => b.DateAdded).ToList();
        }
    }
}
