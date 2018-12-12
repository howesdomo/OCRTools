using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        byte[] CurrentImage { get; set; }

        string MethodName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = this.Title + "- V{0}".FormatWith(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            OCRUtils_Baidu.InitBaiduKey();

            initUI();
            initEvent();
            this.txtImage.Focus();
        }

        private void initUI()
        {

        }

        private void initEvent()
        {
            this.txtImage.PreviewTextInput += (s, e) => { e.Handled = false; };
            this.txtImage.TextChanged += (s, e) =>
            {
                TextRange txt = new TextRange(txtImage.Document.ContentStart, txtImage.Document.ContentEnd);
                if (txt.Text.IsNullOrEmpty() == false)
                {
                    if (txt.Text != " \r\n")
                    {
                        txtImage.Document.Blocks.Clear();
                    }
                }
            };

            this.btnExcute_GeneralBasic.Click += BtnExcute_GeneralBasic_Click;
            this.btnExcute_General.Click += BtnExcute_General_Click;
            this.btnExcute_AccurateBasic.Click += BtnExcute_AccurateBasic_Click;
            this.btnExcute_Accurate.Click += BtnExcute_Accurate_Click;

            DataObject.AddPastingHandler(this.txtImage, pastingEvent);
        }

        private void pastingEvent(object sender, DataObjectPastingEventArgs e)
        {
            if (bgWorker != null && bgWorker.IsBusy == true)
            {
                e.CancelCommand();
                return;
            }

            if (Clipboard.ContainsImage() == true)
            {
                BitmapSource bitmapSource = null;
                try
                {
                    bitmapSource = Clipboard.GetImage();
                    if (bitmapSource != null)
                    {
                        byte[] byteArr_Image = null;

                        using (MemoryStream msOutput = new MemoryStream())
                        {
                            BitmapEncoder enc = new BmpBitmapEncoder();
                            enc.Frames.Add(BitmapFrame.Create(bitmapSource));
                            enc.Save(msOutput);
                            msOutput.Flush();
                            byteArr_Image = msOutput.ToArray();
                        }

                        if (bgWorker == null)
                        {
                            bgWorker = new BackgroundWorker();
                            bgWorker.DoWork += BgWorker_DoWork;
                            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
                        }

                        ucBusy.IsBusy = true;
                        this.MethodName = "GeneralBasic";
                        this.CurrentImage = byteArr_Image;
                        bgWorker.RunWorkerAsync(byteArr_Image);
                    }
                    else
                    {
                        string msg = "{0}".FormatWith("获取图片资源失败.");
                        Action<String> UI_ShowMessageBox_Action = new Action<string>(UI_ShowMessageBox);
                        this.Dispatcher.BeginInvoke(method: UI_ShowMessageBox_Action, args: new object[1] { msg });
                        e.CancelCommand();
                    }
                }
                catch (Exception ex)
                {
                    string msg = "{0}{1}".FormatWith("获取图片资源失败.", ex.Message);                    
                    Action<String> UI_ShowMessageBox_Action = new Action<string>(UI_ShowMessageBox);
                    this.Dispatcher.BeginInvoke(method: UI_ShowMessageBox_Action, args : new object[1] { msg });                    
                    e.CancelCommand();
                }
            }
        }

        

        private void UI_ShowMessageBox(string msg)
        {
            MessageBox.Show(msg, "Error");
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OCRResult r = null;

            switch (this.MethodName)
            {
                case "GeneralBasic": r = OCRUtils_Baidu.Excute_GeneralBasic(e.Argument as byte[]); break;
                case "General": r = OCRUtils_Baidu.Excute_General(e.Argument as byte[]); break;
                case "AccurateBasic": r = OCRUtils_Baidu.Excute_AccurateBasic(e.Argument as byte[]); break;
                case "Accurate": r = OCRUtils_Baidu.Excute_Accurate(e.Argument as byte[]); break;
            }

            e.Result = r;
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ucBusy.IsBusy = false;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.GetFullInfo(), "Error");
            }
            else
            {
                if (e.Result == null)
                {
                    MessageBox.Show("Result is null", "Error");
                    return;
                }

                var r = e.Result as OCRResult;

                if (r.IsComplete == false)
                {
                    MessageBox.Show(r.ExceptionInfo, "Error");
                    return;
                }

                if (r.IsSuccess == false)
                {
                    MessageBox.Show(r.BusinessExceptionInfo, "Business Error");
                    return;
                }

                if (r.Details != null && r.Details.Count > 0)
                {
                    var dt = r.Details.ToDataTable<OCRDetail>();
                    this.dg1.ItemsSource = dt.DefaultView;
                }
                else
                {
                    this.dg1.ItemsSource = null;
                }
            }
        }

        BackgroundWorker bgWorker { get; set; }

        private void BtnExcute_GeneralBasic_Click(object sender, RoutedEventArgs e)
        {
            this.MethodName = "GeneralBasic";
            execute();
        }

        private void BtnExcute_General_Click(object sender, RoutedEventArgs e)
        {
            this.MethodName = "General";
            execute();
        }

        private void BtnExcute_AccurateBasic_Click(object sender, RoutedEventArgs e)
        {
            this.MethodName = "AccurateBasic";
            execute();
        }

        private void BtnExcute_Accurate_Click(object sender, RoutedEventArgs e)
        {
            this.MethodName = "Accurate";
            execute();
        }

        private void execute()
        {
            if (bgWorker != null && bgWorker.IsBusy == true)
            {
                MessageBox.Show("正在失败中", "错误");
                return;
            }
            ucBusy.IsBusy = true;
            bgWorker.RunWorkerAsync(this.CurrentImage);
        }



    }
}
