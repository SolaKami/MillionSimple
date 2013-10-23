using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Model;
using System.Net;
using MillionSimple.Data;
using MillionSimple.Common;

namespace MillionSimple.Work
{
    public class ConnectWork
    {

        #region 属性

        private HttpHelper _http;

        public HttpHelper Http
        {
            get
            {
                if (_http == null)
                {
                    _http = new HttpHelper();
                }
                return _http;
            }
        }



        //用以判断是否记录日志
        public bool HasGetUser { get; set; }

        public string UserName { get; set; }




        #endregion


        #region 动作执行入口
        /// <summary>
        /// 动作执行入口
        /// </summary>
        /// <param name="yase"></param>
        /// <returns></returns>
        public ActResult DoAct(YaSeModel yase)
        {
            HttpRequestParameter reqPara = ParameterWork.GetMAParameter(yase);
            HttpRequestParameter request = GetHttpRequest(reqPara);
            HttpResponseParameter resPara = HttpRequest(request);
            return ProduceActResult(resPara);
        }
        #endregion


        #region 构造返回中枢的结果类
        /// <summary>
        /// 构造返回中枢的结果类
        /// </summary>
        /// <param name="resPara"></param>
        /// <returns></returns>
        private ActResult ProduceActResult(HttpResponseParameter resPara)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region 构造HTTP参数
        /// <summary>
        /// 构造HTTP参数
        /// </summary>
        /// <param name="reqPara"></param>
        /// <returns></returns>
        private HttpRequestParameter GetHttpRequest(HttpRequestParameter reqPara)
        {
            //game1-cbt.ma.sdo.com
            string host = "http://117.121.6.138:10001";
            string url = host + reqPara.RequestUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] byteSend = MaSetting.httpEncode.GetBytes(reqPara.RequestData);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            request.Headers.Set("Accept-Language", "zh-cn");
            request.KeepAlive = true;
            if (!string.IsNullOrEmpty(reqPara.Cookie))
            {
                request.Headers.Set("Cookie", reqPara.Cookie);
                request.Headers.Set("Cookie2", "$Version=1");
            }
            request.Proxy = null;
            request.Timeout = 20 * 1000;
            reqPara.request = request;
            return reqPara;
        }
        #endregion


        #region HTTP请求
        /// <summary>
        /// HTTP请求
        /// </summary>
        /// <param name="actModeEnum"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private HttpResponseParameter HttpRequest(HttpRequestParameter request)
        {
            HttpResponseParameter respara = Http.RequestNew(request);
            #region 记录日志
            if (!HasGetUser)
            {
                UserName = Analyze.GetDefaultUser();
            }
            if (UserName == "admin")
            {
                if (respara.IsRequestSuccess)
                {
                    string content = string.Empty;
                    content += Des.GetTimeDes() + "\r\n";
                    content += string.Format("指令:{0}", request.Act.ToString()) + "\r\n";
                    content += respara.Content;
                    content += "\r\n\r\n";
                    HttpMessageLog.writeLog(content);
                }
                else
                {
                    string content = string.Empty;
                    content += Des.GetTimeDes() + "\r\n";
                    content += string.Format("指令:{0}", request.Act.ToString()) + "\r\n";
                    content += respara.ErrorMsg;
                    content += "\r\n\r\n";
                    HttpMessageLog.writeLog(content);
                }
            }

            #endregion
            return respara;
        }
        #endregion
    }
}
