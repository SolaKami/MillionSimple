using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.CommonEnum;
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

        private string _interfaceUrl;
        /// <summary>
        /// 接口地址
        /// </summary>
        public string InterfaceUrl
        {
            get { return _interfaceUrl; }
            set { _interfaceUrl = value; }
        }

        private string _para;
        /// <summary>
        /// 发送参数   
        /// </summary>
        public string Para
        {
            get { return _para; }
            set { _para = value; }
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

        public HttpWebRequest req { get; set; }

    }
}
