using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.IO.Compression;
using System.Globalization;
using System;
using MillionSimple.Model;
namespace MillionSimple.Common
{
    /// <summary>
    /// HTTP连接辅助类
    /// </summary>
    public class HttpHelper
    {
        #region HTTP REQUEST 请求
        /// <summary>
        /// HTTP REQUEST 请求
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public HttpResponseParameter Request(HttpRequestParameter para)
        {
            HttpResponseParameter resPara = new HttpResponseParameter();

            #region 参数设置
            //game1-cbt.ma.sdo.com
            string host = "http://117.121.6.138:10001";
            string url = host + para.InterfaceUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(para.Para);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = "117.121.6.138:10001";
            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            request.Headers.Set("Accept-Language", "zh-cn");
            request.KeepAlive = true;
            if (!string.IsNullOrEmpty(para.Cookie))
            {
                request.Headers.Set("Cookie", para.Cookie);
                request.Headers.Set("Cookie2", "$Version=1");
            }
            request.Proxy = null;
            request.Timeout = 20 * 1000;
            #endregion
            string result = string.Empty;
            HttpWebResponse response = null;
            HttpResponseParameter res = new HttpResponseParameter();
            try
            {
                Stream streamSend = request.GetRequestStream();
                streamSend.Write(byteSend, 0, byteSend.Length);
                streamSend.Close();
                //接收HTTP做出的响应  
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, CommonSetting.httpEncode);
                ////获取返回的信息
                result = streamReader.ReadToEnd();
                streamReader.Close();

                string time = DateTime.Now.ToString();
                resPara.ReturnTime = time;
                resPara.Content = result;
                resPara.IsRequestSuccess = true;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                resPara.ErrorMsg = ex.Message;
            }
            if (response != null)
            {
                response.Close();
            }
            resPara.WebResponse = response;
            return resPara;
        }

        /// <summary>
        /// HTTP REQUEST 请求 加密模式
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public HttpResponseParameter Request(HttpRequestParameter para, bool isEncrypt)
        {
            HttpResponseParameter resPara = new HttpResponseParameter();

            #region 参数设置
            //game1-cbt.ma.sdo.com
            string host = "http://117.121.6.138:10001";
            string url = host + para.InterfaceUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(para.Para);
            request.Method = "post";
            request.UserAgent = "Million/100 (GT-I9100; GT-I9100; 2.3.4) samsung/GT-I9100/GT-I9100:2.3.4/GRJ22/eng.build.20120314.185218:eng/release-keys";
            request.Headers.Set("Accept-Encoding", "gzip, deflate");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = "117.121.6.138:10001";
            //request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            request.Headers.Set("Accept-Language", "zh-cn");
            request.KeepAlive = true;
            if (!string.IsNullOrEmpty(para.Cookie))
            {
                request.Headers.Set("Cookie", para.Cookie);
                request.Headers.Set("Cookie2", "$Version=1");
            }
            request.Proxy = null;
            request.Timeout = 20 * 1000;
            #endregion
            string result = string.Empty;
            HttpWebResponse response = null;
            HttpResponseParameter res = new HttpResponseParameter();
            try
            {
                Stream streamSend = request.GetRequestStream();
                streamSend.Write(byteSend, 0, byteSend.Length);
                streamSend.Close();
                //接收HTTP做出的响应  
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, CommonSetting.httpEncode);
                ////获取返回的信息
                result = streamReader.ReadToEnd();
                streamReader.Close();

                string time = DateTime.Now.ToString();
                resPara.ReturnTime = time;
                resPara.Content = result;
                resPara.IsRequestSuccess = true;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                resPara.ErrorMsg = ex.Message;
            }
            if (response != null)
            {
                response.Close();
            }
            resPara.WebResponse = response;
            return resPara;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqPara"></param>
        /// <returns></returns>
        public HttpResponseParameter Request2(HttpRequestParameter reqPara)
        {
            HttpResponseParameter resPara = new HttpResponseParameter();
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(reqPara.Para);
            string result = string.Empty;
            HttpWebResponse response = null;
            HttpResponseParameter res = new HttpResponseParameter();
            try
            {
                Stream streamSend = reqPara.req.GetRequestStream();
                streamSend.Write(byteSend, 0, byteSend.Length);
                streamSend.Close();
                //接收HTTP做出的响应  
                response = (HttpWebResponse)reqPara.req.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, CommonSetting.httpEncode);
                ////获取返回的信息
                result = streamReader.ReadToEnd();
                streamReader.Close();
                string time = DateTime.Now.ToString();
                resPara.ReturnTime = time;
                resPara.Content = result;
                resPara.IsRequestSuccess = true;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                resPara.ErrorMsg = ex.Message;
            }
            if (response != null)
            {
                response.Close();
            }
            resPara.WebResponse = response;
            return resPara;
        }
        #endregion
    }
}
