using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests_OCR
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            Util.OCR.OCRUtils_Baidu.InitBaiduKey();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var path = @"D:\SC_Github\OCRTools\OCRSolution\UnitTests_OCR\Images\0~9_A~G.png";

            byte[] byteArr_Image = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path: path, mode: System.IO.FileMode.Open))
            {
                byteArr_Image = new byte[fs.Length];
                fs.Read(byteArr_Image, 0, byteArr_Image.Length);
            }

            var r = Util.OCR.OCRUtils_Baidu.Excute(byteArr_Image);

            Assert.AreEqual<bool>(true, r.IsSuccess);
            Assert.AreEqual<int>(2, r.Details.Count);

            var detail = r.Details[0];
            Assert.AreEqual<string>("1234567890", detail.Content);

            detail = r.Details[1];
            Assert.AreEqual<string>("ABCDEFG", detail.Content);
        }
    }
}
