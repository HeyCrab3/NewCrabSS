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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewCrabSS.window
{
    /// <summary>
    /// Alert.xaml 的交互逻辑
    /// </summary>
    public partial class Alert : UserControl
    {
        public Alert()
        {
            bd nd = new bd();
            nd.InitializeComponent();
            var result = WebHandler.webhandler.GetReq("https://v6.crabapi.cn/api/crabss/broadcast?channel=beta&encode=text");
            nd.xd.Content = result;
        }
    }
}
