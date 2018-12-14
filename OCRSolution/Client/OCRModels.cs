using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [Serializable]
    public class ApiSecurityInfo
    {
        public string ApiID { get; set; }

        public string ApiKey { get; set; }

        public string SecretKey { get; set; }

        public override bool Equals(object obj)
        {
            bool r = false;

            if (obj is ApiSecurityInfo)
            {
                var toMatch = obj as ApiSecurityInfo;

                if (
                    toMatch.ApiID == this.ApiID &&
                    toMatch.ApiKey == this.ApiKey &&
                    toMatch.SecretKey == this.SecretKey
                    )
                {
                    r = true;
                }
                else
                {
                    r = false;
                }
            }
            else
            {
                r = base.Equals(obj);
            }

            return r;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public class OCRResult
    {
        /// <summary>
        /// 方法运行成功
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// 运行报错信息
        /// </summary>
        public string ExceptionInfo { get; set; }

        #region 业务逻辑

        /// <summary>
        /// 业务逻辑运行成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 业务逻辑报错信息
        /// </summary>
        public string BusinessExceptionInfo { get; set; }

        public List<OCRDetail> Details { get; set; }

        #endregion
    }

    public class OCRDetail
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public decimal? Top { get; set; }

        public decimal? Left { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }
    }
}
