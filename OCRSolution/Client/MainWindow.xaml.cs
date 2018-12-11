using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            OCRUtils_Baidu.InitBaiduKey();
            initEvent();
        }

        private void initEvent()
        {
            this.btnExcute.Click += BtnExcute_Click;

            DataObject.AddPastingHandler(this.txtImage, pastingEvent);
        }

        private void pastingEvent(object sender, DataObjectPastingEventArgs e)
        {
            if (Clipboard.ContainsImage() == true)
            {
                BitmapSource bitmapSource = null;
                try
                {
                    bitmapSource = Clipboard.GetImage();
                    if (bitmapSource != null)
                    {
                        img.Source = bitmapSource;
                        // img.Source = new BitmapImage(new Uri(@"D:\HoweDesktop\表.png", UriKind.Absolute));

                        byte[] byteArr_Image = null;

                        using (MemoryStream msOutput = new MemoryStream())
                        {
                            BitmapEncoder enc = new BmpBitmapEncoder();
                            enc.Frames.Add(BitmapFrame.Create(bitmapSource));
                            enc.Save(msOutput);
                            msOutput.Flush();
                            byteArr_Image = msOutput.ToArray();
                        }

                        var r = OCRUtils_Baidu.Excute(byteArr_Image);
                        var dt = r.Details.ToDataTable<OCRDetail>();
                        this.dg1.ItemsSource = dt.DefaultView;

                        e.CancelCommand();
                    }
                    else
                    {
                        string msg = "{0}".FormatWith("获取图片资源失败.");
                        System.Diagnostics.Debug.WriteLine(msg);
                    }
                }
                catch (Exception ex)
                {
                    string msg = "{0}{1}".FormatWith("获取图片资源失败.", ex.Message);
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            }
        }

        private void BtnExcute_Click(object sender, RoutedEventArgs e)
        {
            var path = @"D:\SC_Github\OCRTools\OCRSolution\Client\TestImages\0~9_A~G.png";

            byte[] byteArr_Image = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path: path, mode: System.IO.FileMode.Open))
            {
                byteArr_Image = new byte[fs.Length];
                fs.Read(byteArr_Image, 0, byteArr_Image.Length);
            }

            var r = OCRUtils_Baidu.Excute(byteArr_Image);
            var dt = r.Details.ToDataTable<OCRDetail>();

            this.dg1.ItemsSource = dt.DefaultView;
        }

        //public byte[] BitmapImageToByteArray(BitmapImage bmp)
        //{
        //    byte[] byteArray = null;
        //    try
        //    {
        //        System.IO.Stream sMarket = bmp.StreamSource;
        //        if (sMarket != null && sMarket.Length > 0)
        //        {
        //            //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。 
        //            sMarket.Position = 0;
        //            using (System.IO.BinaryReader br = new System.IO.BinaryReader(sMarket))
        //            {
        //                byteArray = br.ReadBytes((int)sMarket.Length);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        //other exception handling 
        //    }
        //    return byteArray;
        //}


    }
}
