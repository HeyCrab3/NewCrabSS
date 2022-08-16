using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewCrabSS.PluginHandler
{
    internal class PluginHandler
    {
        public static void GetPluginList(ListBox pluginlist, Label counts) {
            try
            {
                String path = @"plugins";
                string[] files = Directory.GetFiles(path, "*.jar");
                string[] dfiles = Directory.GetFiles(path, "*.jar.DISABLED");
                foreach (string file in files)
                {
                    pluginlist.Items.Add(file);
                }
                foreach (string dfile in dfiles)
                {
                    pluginlist.Items.Add(dfile + " (已禁用)");
                }
                counts.Content = "服务器目前安装了 " + pluginlist.Items.Count + " 个插件";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                counts.Content = "服务器目前安装了 N/A 个插件";
            }
        }
        public static void DisablePlugins(ListBox list)
        {
            try
            {
                var filename = list.SelectedValue;
                if (filename != null)
                {
                    if (filename.ToString().Contains(".DISABLED") == true)
                    {
                        File.Move(filename.ToString().Replace("(已禁用)", ""), filename.ToString().Replace(".DISABLED (已禁用)", ""));
                        MessageBox.Show("插件已成功解除禁用", "成功！", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        list.SelectedValue = filename + " (已禁用)";
                        File.Move(filename.ToString(), filename.ToString() + ".DISABLED");
                        MessageBox.Show("插件已成功禁用", "成功！", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("LOWIQ\n选择项是空的","L 主播是LowIQ",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void AddPlugins(ListBox pluginlist, Label counts)
        {
            try
            {
                var of = new Microsoft.Win32.OpenFileDialog();
                of.Filter = "插件文件 (*.jar)|*.jar";
                if (of.ShowDialog() == true)
                {
                    pluginlist.Items.Clear();
                    string pluginPath = System.IO.Path.GetFullPath(of.FileName);
                    string pluginName = of.SafeFileName;
                    File.Copy(pluginPath, "plugins\\" + pluginName);
                    string path = @"plugins";
                    string[] files = Directory.GetFiles(path, "*.jar");
                    foreach (string file in files)
                    {
                        pluginlist.Items.Add(file);
                    }
                    counts.Content = "服务器目前安装了 " + pluginlist.Items.Count + " 个插件";
                    MessageBox.Show("插件已成功安装！", ":D",MessageBoxButton.OK,MessageBoxImage.Information);
                    pluginlist.Items.Clear();
                    GetPluginList(pluginlist, counts);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                counts.Content = "服务器目前安装了 N/A 个插件";
            }
        }
        public static void DeletePlugins(ListBox list)
        {
            try
            {
                var filename = list.SelectedValue;
                if (filename != null)
                {
                    list.Items.Remove(filename);
                    if (filename.ToString().Contains(".DISABLED (已禁用)") == true)
                    {
                        string result = filename.ToString().Replace("(已禁用)", "");
                        File.Delete(result);
                    }
                    File.Delete(filename.ToString());
                    MessageBox.Show("插件已成功删除", "成功！", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("LOWIQ\n选择项是空的", "L 主播是LowIQ", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
