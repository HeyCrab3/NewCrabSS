using System;
using System.Collections.Generic;
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
using libc.hwid;

namespace NewCrabSS.window
{
    /// <summary>
    /// update.xaml 的交互逻辑
    /// </summary>
    public partial class update : Window
    {
        public update()
        {
            InitializeComponent();
            var HWID = HwId.Generate();
            hwid.Content = "您的设备 HWID 是 " + HWID;
        }
    }
}
