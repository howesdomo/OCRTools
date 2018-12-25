using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util.OCR;

namespace UnitTest
{
    [TestClass]
    public class OCRUtils_Baidu_UnitTest
    {
        [TestInitialize]
        public void Init()
        {
            OCRUtils_Baidu.InitBaiduKey();
        }


        [TestMethod]
        public void Test_Excute_GeneralBasic()
        {
            var path = @"D:\SC_Github\OCRTools\OCRSolution\UnitTest\Images\0~9_A~G.png";

            byte[] byteArr_Image = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path: path, mode: System.IO.FileMode.Open))
            {
                byteArr_Image = new byte[fs.Length];
                fs.Read(byteArr_Image, 0, byteArr_Image.Length);
            }

            var r = OCRUtils_Baidu.Excute_GeneralBasic(byteArr_Image);

            Assert.AreEqual<bool>(true, r.IsSuccess);
            Assert.AreEqual<int>(2, r.Details.Count);

            var detail = r.Details[0];
            Assert.AreEqual<string>("1234567890", detail.Content);

            detail = r.Details[1];
            Assert.AreEqual<string>("ABCDEFG", detail.Content);
        }

        [TestMethod]
        public void Test_Excute_General()
        {
            var path = @"D:\SC_Github\OCRTools\OCRSolution\UnitTest\Images\0~9_A~G.png";

            byte[] byteArr_Image = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path: path, mode: System.IO.FileMode.Open))
            {
                byteArr_Image = new byte[fs.Length];
                fs.Read(byteArr_Image, 0, byteArr_Image.Length);
            }

            var r = OCRUtils_Baidu.Excute_General(byteArr_Image);

            Assert.AreEqual<bool>(true, r.IsSuccess);
            Assert.AreEqual<int>(2, r.Details.Count);

            var detail = r.Details[0];
            Assert.AreEqual<string>("1234567890", detail.Content);

            detail = r.Details[1];
            Assert.AreEqual<string>("ABCDEFG", detail.Content);
        }

        [TestMethod]
        public void Test_Excute_AccurateBasic()
        {
            var path = @"D:\SC_Github\OCRTools\OCRSolution\UnitTest\Images\0~9_A~G.png";

            byte[] byteArr_Image = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path: path, mode: System.IO.FileMode.Open))
            {
                byteArr_Image = new byte[fs.Length];
                fs.Read(byteArr_Image, 0, byteArr_Image.Length);
            }

            var r = OCRUtils_Baidu.Excute_AccurateBasic(byteArr_Image);

            Assert.AreEqual<bool>(true, r.IsSuccess);
            Assert.AreEqual<int>(2, r.Details.Count);

            var detail = r.Details[0];
            Assert.AreEqual<string>("1234567890", detail.Content);

            detail = r.Details[1];
            Assert.AreEqual<string>(" ABCDEFG", detail.Content);
        }

        [TestMethod]
        public void Test_Excute_Accurate()
        {
            var path = @"D:\SC_Github\OCRTools\OCRSolution\UnitTest\Images\0~9_A~G.png";

            byte[] byteArr_Image = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path: path, mode: System.IO.FileMode.Open))
            {
                byteArr_Image = new byte[fs.Length];
                fs.Read(byteArr_Image, 0, byteArr_Image.Length);
            }

            var r = OCRUtils_Baidu.Excute_Accurate(byteArr_Image);

            Assert.AreEqual<bool>(true, r.IsSuccess);
            Assert.AreEqual<int>(2, r.Details.Count);

            var detail = r.Details[0];
            Assert.AreEqual<string>("1234567890", detail.Content);

            detail = r.Details[1];
            Assert.AreEqual<string>(" ABCDEFG", detail.Content);
        }
    }
}
