using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static string FileName = "BaiduAIKeys.json";

        /// <summary>
        /// 目录
        /// </summary>
        public static string DirectoryPath = System.IO.Path.Combine
        (
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            , "HoweSoftware"
            , "OCRTools"
        );

        /// <summary>
        /// 文件路径
        /// </summary>
        public static string FullName = System.IO.Path.Combine
        (
              App.DirectoryPath
            , App.FileName
        );

        protected override void OnStartup(StartupEventArgs e)
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            System.IO.Directory.CreateDirectory(App.DirectoryPath);
            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                HandleException(e.Exception);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public static void HandleException(Exception ex)
        {
            string caption = "发生未知错误。请与管理员联系以获取帮助。";
            Util.LogUtils.Log(ex.GetFullInfo());
            MessageBox.Show(ex.GetFullInfo(), caption);
        }
    }
}
