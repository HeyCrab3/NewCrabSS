using libc.hwid;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace NewCrabSS.window
{
    /// <summary>
    /// HWIDverify.xaml 的交互逻辑
    /// </summary>
    public partial class HWIDverify : Window
    {
        public static string HWID = HwId.Generate();
        public HWIDverify()
        {
            InitializeComponent();
            hwidtip.Content = hwidtip.Content + HWID;
            hwidtip.Uid = HWID;
        }

        private void gotoa(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer","https://api-crabss.crabapi.cn/user/reg");
        }

        private void verify_Click(object sender, RoutedEventArgs e)
        {
            verify.Content = "Loading...";
            var result = WebHandler.webhandler.GetReq("https://api-crabss.crabapi.cn/application/v1/hwid/auth?hwid=" + HWID);
            ConfigProvider.ConfigProvider.HWID b = JsonConvert.DeserializeObject<ConfigProvider.ConfigProvider.HWID>(result);
            if (b.code != 0)
            {
                verify.Content = "点我进行HWID验证";
                info.Content = b.msg;
                L.IsOpen = true;
            }
            else
            {
                MainWindow mw = new();
                Close();
                mw.ShowDialog();
            }
        }

        private void copy(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(HWID);
            v.Content = "已复制！";
        }
        private void close(object sender, RoutedEventArgs e)
        {
            L.IsOpen = false;
        }
    }
}
