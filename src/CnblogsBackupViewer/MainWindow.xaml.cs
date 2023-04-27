using CnblogsBackupViewer.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CnblogsBackupViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataProvider m_DataProvider;
        string m_BlogTemplate;

        public MainWindow()
        {
            InitializeComponent();

            Title = $"{Constants.AppName} {Constants.AppVersion}";
        }

        private void UI_Menu_File_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UI_Menu_Help_About_Click(object sender, RoutedEventArgs e)
        {
            var content = $"{Constants.AppName} {Constants.AppVersion}{Environment.NewLine}Copyright ©2023 wenhx.";
            MessageBox.Show(content, $"About {Constants.AppName}");
        }

        private void UI_Menu_File_Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "cnblogs backup files|*.db;*.xml;*.json";
            if (dialog.ShowDialog() == false)
                return;

            var file = dialog.FileName;
            if (File.Exists(file) == false)
            {
                MessageBox.Show("文件不存在。");
                return;
            }

            try
            {
                m_DataProvider = DataProviderFactory.Create(file);
                UpdateWindowTitle(m_DataProvider.Site);
                UpdateListBoxItemsSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            void UpdateListBoxItemsSource()
            {
                var index = 1;
                UI_ListBox_Blogs.ItemsSource = m_DataProvider.Blogs.Select(b => new BlogItem
                {
                    Id = b.Id,
                    Index = "#" + index++,
                    Title = b.Title,
                });
            }

            void UpdateWindowTitle(Site site)
            {
                var title = string.IsNullOrWhiteSpace(site.Title) ? // XML 备份中没有这个字段。
                    string.Format("{0} 的博客 - {1}", site.Author, this.Title) :
                    string.Format("{0} - {1} - {2}", site.Title, site.Author, this.Title);
                this.Title = title;
            }
        }

        void UI_ListBox_Blogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (UI_ListBox_Blogs.SelectedItem as BlogItem)!;
            var blog = m_DataProvider.Blogs.Where(b => b.Id == selectedItem.Id).FirstOrDefault();
            if (blog == null)
            {
                UI_WebBrowser.NavigateToString("文章不存在，可能被删除了。"); //非中文操作系统下可能会显示乱码
            }
            else
            {
                var blogContent = WrapWithHTMLTemplate(blog.Body);
                UI_WebBrowser.NavigateToString(blogContent);
            }

            string WrapWithHTMLTemplate(string body)
            {
                try
                {
                    if (m_BlogTemplate == null)
                    {
                        m_BlogTemplate = File.ReadAllText("BlogTemplate.txt");
                    }
                    var blogContent = m_BlogTemplate.Replace("{BlogBody}", body);
                    return blogContent;
                }
                catch (Exception ex)
                {
                    var message = $"加载HTML模板时发生错误，请检查程序运行目录是否存在BlogTemplate.txt文件。博客内容将以没有样式的形式显示。{Environment.NewLine}{Environment.NewLine}错误消息: {ex.Message}";
                    MessageBox.Show(message);
                    return body;
                }
            }
        }

        public class BlogItem
        {
            public int Id { get; set; }
            public string Index { get; set; } = String.Empty;
            public string Title { get; set; } = String.Empty;
        }
    }
}
