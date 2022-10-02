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

namespace NewCrabSS.CustomizeControls
{
    /// <summary>
    /// 一个自定义的消息提示框
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox(string title="Unknown", string info="text", string icon="Information")
        {
            InitializeComponent();
            List<Message> messageInfo = new List<Message>()
            {
                new Message() { title = title, text = info, icon = icon },
            };
            Binding binding = new();
            binding.Source = messageInfo;
            binding.Path = new PropertyPath("title");
            BindingOperations.SetBinding(this.title, ContentProperty, binding);
            BindingOperations.SetBinding(this.title, TitleProperty, binding);
            Binding binding1 = new();
            binding1.Source = messageInfo;
            binding1.Path = new PropertyPath("text");
            BindingOperations.SetBinding(this.text, ContentProperty, binding1);
            if (icon == "Information")
            {
                this.icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.InformationOutline;
            }
            else if (icon == "Warning")
            {
                this.icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WarningCircleOutline;
            }
            else if (icon == "Error")
            {
                this.icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ErrorOutline;
            }
            else
            {
                this.icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.QuestionMarkCircle;
            }
        }
        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();    
        }
        /**private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }**/
        public class Message
        {
            public string title { get; set; }
            public string text { get; set; }
            public string icon { get; set; }
        }
    }
}
