namespace CnblogsBackupViewer.Data
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }

        public string Body { get; set; } = string.Empty;
    }
}