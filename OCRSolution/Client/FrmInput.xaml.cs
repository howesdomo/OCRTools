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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// FrmInput.xaml 的交互逻辑
    /// </summary>
    public partial class FrmInput : Window
    {
        public bool IsSubmit { get; set; }

        public string ApiID { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }

        public FrmInput()
        {
            InitializeComponent();
            initEvent();
        }

        private void initEvent()
        {
            this.Closing += Frm_Closing;
            this.btnSumbit.Click += BtnSumbit_Click;
        }

        private void Frm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void BtnSumbit_Click(object sender, RoutedEventArgs e)
        {
            string errorMsg = check();
            if (errorMsg.IsNullOrWhiteSpace())
            {
                this.ApiID = this.txtApiID.Text;
                this.ApiKey = this.txtApiKey.Text;
                this.SecretKey = this.txtSecretKey.Text;

                this.IsSubmit = true;

                this.Close();
            }
        }

        private string check()
        {
            if (this.txtApiID.Text.IsNullOrWhiteSpace())
            {
                return "请输入{0}".FormatWith("ApiID");
            }

            if (this.txtApiKey.Text.IsNullOrWhiteSpace())
            {
                return "请输入{0}".FormatWith("ApiKey");
            }

            if (this.txtSecretKey.Text.IsNullOrWhiteSpace())
            {
                return "请输入{0}".FormatWith("SecretKey");
            }

            return string.Empty;
        }

    }
}
