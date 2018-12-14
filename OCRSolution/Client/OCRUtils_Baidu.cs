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

        public static void SetBaiduKey(ApiSecurityInfo i)
        {
            ApiID = i.ApiID;
            ApiKey = i.ApiKey;
            SecretKey = i.SecretKey;
        }

        /// <summary>
        /// 通用文字识别
        /// </summary>
        /// <param name="byteArr_Image"></param>
        /// <returns></returns>
        public static OCRResult Excute_GeneralBasic(byte[] byteArr_Image)
        {
            OCRResult r = null;
            try
            {
                var options = new Dictionary<string, object>
                {
                    {"language_type", "CHN_ENG"}, //
                    {"detect_direction", "false"}, // 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:
                    {"detect_language", "true"}, // 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语）
                    {"probability", "false"} // 是否返回识别结果中每一行的置信度
                };

                Ocr client = new Ocr(ApiKey, SecretKey);
                Newtonsoft.Json.Linq.JObject ocrResult = client.GeneralBasic(image: byteArr_Image, options: options);
                r = getOCRResult(ocrResult);
            }
            catch (Exception ex)
            {
                r = new OCRResult();
                r.IsComplete = false;
                r.ExceptionInfo = ex.GetFullInfo();
                r.IsSuccess = false;
            }

            return r;
        }

        /// <summary>
        /// 通用文字识别（含位置信息版）
        /// </summary>
        /// <param name="byteArr_Image"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static OCRResult Excute_General(byte[] byteArr_Image)
        {
            OCRResult r = null;
            try
            {
                var options = new Dictionary<string, object>
                {
                    {"recognize_granularity", "big"}, // 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置
                    {"language_type", "CHN_ENG"}, // 识别语言类型，默认为CHN_ENG。可选值包括：
                    {"detect_direction", "false"}, // 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:
                    {"detect_language", "false"}, // 是否检测语言，默认不检测。当前支持（中文、英语、日语、韩语）
                    {"vertexes_location", "false"}, // 是否返回文字外接多边形顶点位置，不支持单字位置。默认为false
                    {"probability", "false"} // 是否返回识别结果中每一行的置信度
                };

                Ocr client = new Ocr(ApiKey, SecretKey);
                Newtonsoft.Json.Linq.JObject ocrResult = client.General(image: byteArr_Image, options: options);
                r = getOCRResult(ocrResult);
            }
            catch (Exception ex)
            {
                r = new OCRResult();
                r.IsComplete = false;
                r.ExceptionInfo = ex.GetFullInfo();
                r.IsSuccess = false;
            }

            return r;
        }

        /// <summary>
        /// 通用文字识别（高精度版）
        /// </summary>
        /// <param name="byteArr_Image"></param>
        /// <returns></returns>
        public static OCRResult Excute_AccurateBasic(byte[] byteArr_Image)
        {
            OCRResult r = null;
            try
            {
                var options = new Dictionary<string, object>
                {
                    {"detect_direction", "false"}, // 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:
                    {"probability", "false"} // 是否返回识别结果中每一行的置信度
                };

                Ocr client = new Ocr(ApiKey, SecretKey);
                Newtonsoft.Json.Linq.JObject ocrResult = client.AccurateBasic(image: byteArr_Image, options: options);
                r = getOCRResult(ocrResult);
            }
            catch (Exception ex)
            {
                r = new OCRResult();
                r.IsComplete = false;
                r.ExceptionInfo = ex.GetFullInfo();
                r.IsSuccess = false;
            }

            return r;
        }

        /// <summary>
        /// 通用文字识别（含位置高精度版）
        /// </summary>
        /// <param name="byteArr_Image"></param>
        /// <returns></returns>
        public static OCRResult Excute_Accurate(byte[] byteArr_Image)
        {
            OCRResult r = null;
            try
            {
                var options = new Dictionary<string, object>
                {
                    {"recognize_granularity", "big"}, // 是否定位单字符位置，big：不定位单字符位置，默认值；small：定位单字符位置
                    {"detect_direction", "false"}, // 是否检测图像朝向，默认不检测，即：false。朝向是指输入图像是正常方向、逆时针旋转90/180/270度。可选值包括:
                    {"vertexes_location", "false"}, // 是否返回文字外接多边形顶点位置，不支持单字位置。默认为false
                    {"probability", "false"} // 是否返回识别结果中每一行的置信度
                };

                Ocr client = new Ocr(ApiKey, SecretKey);
                Newtonsoft.Json.Linq.JObject ocrResult = client.Accurate(image: byteArr_Image, options: options);
                r = getOCRResult(ocrResult);
            }
            catch (Exception ex)
            {
                r = new OCRResult();
                r.IsComplete = false;
                r.ExceptionInfo = ex.GetFullInfo();
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

                try
                {
                    detail.Width = Convert.ToDecimal(match["location"]["width"].ToString());
                    detail.Height = Convert.ToDecimal(match["location"]["height"].ToString());
                    detail.Top = Convert.ToDecimal(match["location"]["top"].ToString());
                    detail.Left = Convert.ToDecimal(match["location"]["left"].ToString());
                }
                catch (Exception ex)
                {
                    string msg = "{0}".FormatWith(ex.GetFullInfo());
                    System.Diagnostics.Debug.WriteLine(msg);
                }

                r.Details.Add(detail);
            }

            r.IsComplete = true;
            r.IsSuccess = true;

            return r;
        }

    }


}
