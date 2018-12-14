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
        UcWait_ViewModel ViewModel { get; set; }

        public UcWait()
        {
            InitializeComponent();

            this.ViewModel = new UcWait_ViewModel();
            this.DataContext = this.ViewModel;

            initEvent();
        }

        private void initEvent()
        {
            // MediaElement 只能使用绝对路径? 用程序内部资源加载失败
            var uri = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "Images", "wait.gif");
            meWait.Source = new Uri(uri);
            meWait.MediaEnded += (s, e) =>
            {
                meWait.Position = new TimeSpan(hours: 0, minutes: 0, seconds: 1);
                meWait.Play();
            };
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

            this.ViewModel.BusyContent = this.ViewModel._Defalut_BusyContent_;
        }

        private bool _IsBusy = false;

        public bool IsBusy
        {
            get
            {
                return this._IsBusy;
            }
            set
            {
                this._IsBusy = value;
                execute();
            }
        }

        public string BusyContent
        {
            get
            {
                return this.ViewModel.BusyContent;
            }
            set
            {
                this.ViewModel.BusyContent = value;
            }
        }

    }

    public class UcWait_ViewModel : ViewModel.BaseViewModel
    {
        public string _Defalut_BusyContent_ = "请稍候...";

        private string _BusyContent = "请稍候...";

        public string BusyContent
        {
            get
            {
                return _BusyContent;
            }
            set
            {
                _BusyContent = value;
                this.OnPropertyChanged("BusyContent");
            }
        }

    }

}
