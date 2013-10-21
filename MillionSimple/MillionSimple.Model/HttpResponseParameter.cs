using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MillionSimple.Model
{
    public class HttpResponseParameter
    {
        private bool _isRequestSuccess = false;
        /// <summary>
        /// 连接是否成功
        /// </summary>
        public bool IsRequestSuccess
        {
            get { return _isRequestSuccess; }
            set { _isRequestSuccess = value; }
        }

        private string _errorCode;
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        private string _content;
        /// <summary>
        /// 返回字符
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private string _returnTime;
        /// <summary>
        /// 返回时间
        /// </summary>
        public string ReturnTime
        {
            get { return _returnTime; }
            set { _returnTime = value; }
        }


        private HttpWebResponse _webResponse;
        /// <summary>
        /// WEB RESPONSE
        /// </summary>
        public HttpWebResponse WebResponse
        {
            get { return _webResponse; }
            set { _webResponse = value; }
        }


        private string _errorMsg;
        /// <summary>
        /// 抛出错误信息
        /// </summary>
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }


    }
}
