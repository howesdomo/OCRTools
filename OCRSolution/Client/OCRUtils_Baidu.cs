using Baidu.Aip.Ocr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class OCRUtils_Baidu
    {
        private static string ApiID { get; set; }
        private static string ApiKey { get; set; }
        private static string SecretKey { get; set; }

        public static void InitBaiduKey()
        {
            ApiID = "15131270";
            ApiKey = "PXCvUScAN2gYz3h0uaqosxaX";
            SecretKey = "2ttcyrZ0m4uqXRqBhsqBkT6kfGLj60Ga";
        }

        private static System.Collections.Generic.Dictionary<string, object> _sDefalutOptions = new System.Collections.Generic.Dictionary<string, object>()
        {
            //是否定位单字符位置 ,big：不定位单字符位置，默认值；small：定位单字符位置
            //{"recognize_granularity", "big"},
            // 识别语言类型 ,默认为CHN_ENG
            {"language_type", "CHN_ENG"},  
            //是否检测图像朝向，默认不检测
            {"detect_direction", "true"},
            //是否检测语言，默认不检测
            {"detect_language", "true"},
            //是否返回文字外接多边形顶点位置，不支持单字位置。默认为false
            //{"vertexes_location", "true"},
            //是否返回识别结果中每一行的置信度
            //{"probability", "true"}
        };

        public static System.Collections.Generic.Dictionary<string, object> sDefalutOptions
        {
            get
            {
                return _sDefalutOptions;
            }
        }

        public static OCRResult Excute(byte[] byteArr_Image, System.Collections.Generic.Dictionary<string, object> options = null)
        {
            OCRResult r = null;
            try
            {
                if (options == null)
                {
                    options = sDefalutOptions;
                }
                Ocr client = new Ocr(ApiKey, SecretKey);
                Newtonsoft.Json.Linq.JObject ocrResult = client.General(image: byteArr_Image, options: options);
                r = getOCRResult(ocrResult);
            }
            catch (Exception ex)
            {
                r = new OCRResult();
                r.IsComplete = false;
                // r.ExceptionInfo = ex.GetFullInfo();
                r.ExceptionInfo = ex.Message;
                r.IsSuccess = false;
            }

            return r;
        }

        private static OCRResult getOCRResult(Newtonsoft.Json.Linq.JObject item)
        {
            int count = Convert.ToInt32(item["words_result_num"]);

            OCRResult r = new OCRResult()
            {
                Details = new List<OCRDetail>()
            };

            for (int index = 0; index < count; index++)
            {
                var match = item["words_result"][index];
                OCRDetail detail = new OCRDetail();
                detail.Seq = index + 1;
                detail.Content = match["words"].ToString();

                detail.Width = Convert.ToDecimal(match["location"]["width"].ToString());
                detail.Height = Convert.ToDecimal(match["location"]["height"].ToString());
                detail.Top = Convert.ToDecimal(match["location"]["top"].ToString());
                detail.Left = Convert.ToDecimal(match["location"]["left"].ToString());

                r.Details.Add(detail);
            }

            r.IsComplete = true;
            r.IsSuccess = true;

            return r;
        }
    }

   
}
