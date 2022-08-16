using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NewCrabSS.ServerHandler
{
    internal class ServerHandler
    {
        public static string time = DateTime.Now.ToLocalTime().ToString();
        public static MainWindow mw = new();
        public static void BrowseJava(TextBox path)
        {
            var of = new Microsoft.Win32.OpenFileDialog();
            of.Filter = "可执行文件(*.exe)|*.exe";
            of.FilterIndex = 0;
            of.Title = "选择一个Java运行时环境，或者返回直接填入java";
            if (of.ShowDialog() == true)
            {
                 path.Text = of.FileName;
            }
        }
        public static void copyCore(TextBox path)
        {
            var of = new Microsoft.Win32.OpenFileDialog();
            of.Filter = "内核文件(*.jar)|*.jar";
            of.FilterIndex = 0;
            of.Title = "选择你的服务器内核";
            if (of.ShowDialog() == true)
            {
                try
                {
                    File.Copy(of.FileName, of.SafeFileName);
                    path.Text = of.SafeFileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
