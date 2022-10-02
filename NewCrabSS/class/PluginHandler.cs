using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MessageBox = NewCrabSS.CustomizeControls.MessageBox;

namespace NewCrabSS.PluginHandler
{
    internal class PluginHandler
    {
        public static void GetPluginList(ListBox pluginlist, Label counts) {
            try
            {
                string path = @"plugins";
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
                MessageBox messageBoxInstance = new($"抛出异常：{ex.Message}", $"无法正常读取插件列表。可能是因为服务器未启动过？", "Error");
                messageBoxInstance.Show();
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
                        MessageBox messageBoxInstance = new("成功", $"插件已成功解除禁用！", "Information");
                        messageBoxInstance.Show();
                    }
                    else
                    {
                        list.SelectedValue = filename + " (已禁用)";
                        File.Move(filename.ToString(), filename.ToString() + ".DISABLED");
                        MessageBox messageBoxInstance = new("成功", $"插件已成功禁用！", "Information");
                        messageBoxInstance.Show();
                    }
                }
                else
                {
                    MessageBox messageBoxInstance = new($"NULL", $"宁的IQ？", "Error");
                    messageBoxInstance.Show();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox messageBoxInstance = new($"抛出异常：{ex.Message}", $"发生了未知错误。\n{ex}", "Error");
                messageBoxInstance.Show();
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
                    MessageBox messageBoxInstance = new("成功", $"插件已成功安装！", "Information");
                    messageBoxInstance.Show();
                    pluginlist.Items.Clear();
                    GetPluginList(pluginlist, counts);
                }
            }
            catch(Exception ex)
            {
                MessageBox messageBoxInstance = new($"抛出异常：{ex.Message}", $"发生了未知错误。\n{ex}", "Error");
                messageBoxInstance.Show();
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
                    MessageBox messageBoxInstance = new("成功", $"插件已成功删除！", "Information");
                    messageBoxInstance.Show();
                }
                else
                {
                    MessageBox messageBoxInstance = new($"NULL", $"宁的IQ？");
                    messageBoxInstance.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox messageBoxInstance = new($"抛出异常：{ex.Message}", $"发生了未知错误。\n{ex}", "Error");
                messageBoxInstance.Show();
            }
        }
    }
}
