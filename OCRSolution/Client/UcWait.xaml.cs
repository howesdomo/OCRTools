using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// UcWait.xaml 的交互逻辑
    /// </summary>
    public partial class UcWait : UserControl
    {
        public UcWait()
        {
            InitializeComponent();

            // MediaElement 只能使用绝对路径???? 用程序内部资源加载失败
            var uri = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "Images", "wait.gif");
            meWait.Source = new Uri(uri);
            meWait.MediaEnded += GifMedia_MediaEnded;
        }

        private void GifMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            meWait.Position = new TimeSpan(0, 0, 1);
            meWait.Play();
        }

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                this.execute();
            }
        }

        private void execute()
        {
            if (this.IsBusy)
            {
                this.gWait.Visibility = Visibility.Visible;
            }
            else
            {
                this.gWait.Visibility = Visibility.Hidden;
            }            
        }

    }
}
