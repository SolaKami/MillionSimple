using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Enum;
using System.Net;

namespace MillionSimple.Model
{
    public class HttpRequestParameter
    {
        private ActModeEnum _act;
        /// <summary>
        /// 行动指令
        /// </summary>
        public ActModeEnum Act
        {
            get { return _act; }
            set { _act = value; }
        }

        private string _requestUrl;
        /// <summary>
        /// 接口地址
        /// </summary>
        public string RequestUrl
        {
            get { return _requestUrl; }
            set { _requestUrl = value; }
        }

        private string _requestData;
        /// <summary>
        /// 发送参数   
        /// </summary>
        public string RequestData
        {
            get { return _requestData; }
            set { _requestData = value; }
        }

        private string _cookie;
        /// <summary>
        /// 使用COOKIE
        /// </summary>
        public string Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        public HttpWebRequest request { get; set; }

    }
}
