using CnblogsBackupViewer.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                var index = 1;
                UI_ListBox_Blogs.ItemsSource = m_DataProvider.Blogs.Select(b => new 
                {
                    Id = b.Id,
                    Index = "#" + index++,
                    Title = b.Title,
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
