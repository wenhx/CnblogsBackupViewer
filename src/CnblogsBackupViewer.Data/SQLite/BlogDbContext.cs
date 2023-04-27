using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnblogsBackupViewer.Data.SQLite
{
    internal class BlogDbContext : DbContext
    {
        static readonly string _ConnectionStringTemplate = "Data Source={0};Cache=Shared";
        readonly string m_DataFilePath;

        public BlogDbContext(string dataFilePath)
        {
            m_DataFilePath = dataFilePath;
        }

        public DbSet<Blog> Blogs { get; set;}

        public DbSet<Site> Sites { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = string.Format(_ConnectionStringTemplate, m_DataFilePath);
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ToTable("blog_Content");
            modelBuilder.Entity<Site>().ToTable("blog_Config");
        }
    }
}
