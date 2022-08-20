using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using ThirdParty.Json.LitJson;

namespace NewCrabSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string time = DateTime.Now.ToLocalTime().ToString();
        public static MainWindow mw = new();
        public static Process p = new Process();
        public SnackbarMessageQueue snackbarMessageQueue = new();
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("idontwannaseeads.inf"))
            {
                ad1.IsEnabled = false;
                closead.IsChecked = true;
            }
            if (File.Exists("config.json"))
            {
                try
                {
                    ConfigProvider.ConfigProvider.Config configprovider = new();
                    using (StreamReader readFile = File.OpenText("config.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(readFile)) //using Newtonsoft.Json
                        {
                            JObject oJson = (JObject)JToken.ReadFrom(reader);
                            #pragma warning disable CS8602 // 解引用可能出现空引用。
                            int confVersion = configprovider.confVersion = (int)oJson["confVersion"];
                            string javaPath = configprovider.javaPath = oJson["javaPath"].ToString();
                            string minRam1 = configprovider.javaPath = oJson["minRam"].ToString();
                            string maxRam1 = configprovider.javaPath = oJson["maxRam"].ToString();
                            string others = configprovider.javaPath = oJson["other"].ToString();
                            #pragma warning restore CS8602 // 解引用可能出现空引用。
                            reader.Close();
                            path.Text = javaPath;
                            minRam.Text = minRam1;
                            maxRam.Text = maxRam1;
                            other.Text = others;
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("发生了关键错误，程序无法启动。\n" + ex.ToString(), "LLLLLL Critical Failure " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            var result = WebHandler.webhandler.GetReq("https://v6.crabapi.cn/api/crabss/broadcast?channel=beta&encode=text");
            xd.Content = result;
            PluginHandler.PluginHandler.GetPluginList(plist, count);
        }

        private void minimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            window.About about = new();
            about.ShowDialog();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            window.update upd = new();
            upd.ShowDialog();
        }
        private void afdian_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer","https://afdian.net/@HeyCrab");
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            PluginHandler.PluginHandler.AddPlugins(plist, count);
        }

        private void disable_Click(object sender, RoutedEventArgs e)
        {
            PluginHandler.PluginHandler.DisablePlugins(plist);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            PluginHandler.PluginHandler.DeletePlugins(plist);
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            plist.Items.Clear();
            PluginHandler.PluginHandler.GetPluginList(plist, count);
        }
        private void start(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("start.cmd"))
            {
                MessageBox.Show("没有对应的脚本文件，请检查。", "L FileNotExist", MessageBoxButton.OK, MessageBoxImage.Error);
                boot.IsEnabled = true;
            }
            else
            {
                boot.IsEnabled = false;
                p.StartInfo.FileName = "start.cmd";
                p.StartInfo.Arguments = "start.cmd";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardError = true;
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputHandler);
            }
        }

        private void light(object sender, RoutedEventArgs e)
        {
            ThemeHandler.CustomizeSettings.ChangeTheme("light");
        }
        private void dark(object sender, RoutedEventArgs e)
        {
            ThemeHandler.CustomizeSettings.ChangeTheme("dark");
        }

        private void browse(object sender, RoutedEventArgs e)
        {
            ServerHandler.ServerHandler.BrowseJava(path);
        }

        private void import(object sender, RoutedEventArgs e)
        {
            ServerHandler.ServerHandler.copyCore(core);
        }

        private void writeScript(object sender, RoutedEventArgs e)
        {
            var content = '"' + path.Text + '"' + " -Xms" + minRam.Text + "M -Xmx" + maxRam.Text + "M -jar " + core.Text + " " + other.Text;
            try
            {
                File.WriteAllText("start.cmd", content);
                snackbarMessageQueue.Enqueue("脚本写入成功，现在可以前往控制台页面启动服务器了 :)");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void writeProp(object sender, RoutedEventArgs e)
        {
            ConfigProvider.ConfigProvider.Config settings = new();
            string json = JsonMapper.ToJson(settings); //using LitJson
            StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\config.json");
            sw.Write(json);
            sw.Dispose();
            string strJson = File.ReadAllText("config.json", Encoding.UTF8);
            JObject oJson = JObject.Parse(strJson); //using Newtonsoft.Json.Linq
            oJson["confVersion"] = 1;
            oJson["minRam"] = minRam.Text;
            oJson["maxRam"] = maxRam.Text;
            oJson["javaPath"] = @path.Text;
            oJson["other"] = other.Text;
            string strConvert = Convert.ToString(oJson); //将json装换为string
            File.WriteAllText("config.json", strConvert); //将内容写进json文件中
            snackbarMessageQueue.Enqueue("配置文件写入成功，下次应用程序启动时将会自动读取");
        }

        private void loadProp(object sender, RoutedEventArgs e)
        {
            try
            {
                var content = File.ReadAllText("server.properites");
                confContainer.Text = content;
                refreshPropBtn.IsEnabled = true;
                savePropBtn.IsEnabled = true;
                loadPropBtn.IsEnabled = false;
                progress.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("配置文件不存在，请确认是否开启过服务器。","L",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void refreshProp(object sender, RoutedEventArgs e)
        {
            confContainer.Text = null;
            try
            {
                var content = File.ReadAllText("server.properites");
                confContainer.Text = content;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void saveProp(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText("server.properites", confContainer.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ProcessOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            try
            {
                Action methodDelegate = delegate ()
                {
                    if (!string.IsNullOrEmpty(outLine.Data))
                    {
                        TextRange tr = new(cons.Document.ContentEnd, cons.Document.ContentEnd);
                        tr.Text = "\n[" + time + "] " + outLine.Data + "\r";
                        Brush brush = Brushes.White;
                        if (outLine.Data.IndexOf("INFO") != -1)
                            brush = Brushes.White;
                        else if (outLine.Data.IndexOf("WARN") != -1)
                        {
                            System.Drawing.Color drawColor = System.Drawing.Color.FromArgb(255, 245, 228, 0);
                            brush = new SolidColorBrush(Color.FromArgb(drawColor.A, drawColor.R, drawColor.G, drawColor.B));
                        }
                        else if (outLine.Data.IndexOf("ERROR") != -1)
                        {
                            System.Drawing.Color drawColor = System.Drawing.Color.FromArgb(255, 214, 0, 21);
                            brush = new SolidColorBrush(Color.FromArgb(drawColor.A, drawColor.R, drawColor.G, drawColor.B));
                        }
                        else
                            brush = Brushes.White;
                        tr.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
                        cons.ScrollToEnd();

                    }

                };
                Dispatcher.BeginInvoke(methodDelegate);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void sendcmd(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.Text.Contains('/'))
                {
                    p.StandardInput.WriteLine(data.Text.Replace("/", string.Empty));
                }
                else
                {
                    p.StandardInput.WriteLine("say " + data.Text);
                }
                p.StandardInput.AutoFlush = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法发送指令，请先启动服务器。如果服务器已启动，请尝试重启进程！\n" + ex.ToString(), "L " + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void writeEula(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("eula.txt", "# 这是通过 CrabSS Reborn 生成的 EULA 文件。该文件不具有标示性，拷至其他服务器仍可免同意 EULA。\n#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\neula = true");
        }

        private void closead_Checked(object sender, RoutedEventArgs e)
        {
            if (closead.IsChecked == true)
            {
                if (File.Exists("idontwannaseeads.inf"))
                {
                    return;
                }
                else
                {
                    File.Create("idontwannaseeads.inf");
                    MessageBox.Show("广告已经关闭啦~\n就是...能不能请螃蟹喝杯咖啡呢（搓手手）...毕竟开发也有成本嘛", ":)", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            if (closead.IsChecked == false)
            {
                File.Delete("idontwannaseeads.inf");
                MessageBox.Show("感谢你打开了广告~\n螃蟹有收入了，能更好的开发啦~", ":)", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
