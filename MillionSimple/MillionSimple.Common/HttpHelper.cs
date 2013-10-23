using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.IO.Compression;
using System.Globalization;
using System;
using MillionSimple.Model;
using System.Diagnostics;
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
        [Obsolete("无效方法")]
        public HttpResponseParameter Request(HttpRequestParameter para)
        {
            HttpResponseParameter resPara = new HttpResponseParameter();

            #region 参数设置
            //game1-cbt.ma.sdo.com
            string host = "http://117.121.6.138:10001";
            string url = host + para.RequestUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(para.RequestData);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
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


        public HttpResponseParameter RequestNew(HttpRequestParameter reqPara)
        {
            HttpWebRequest request = reqPara.request;
            HttpResponseParameter resPara = new HttpResponseParameter();
            string result = string.Empty;
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(reqPara.RequestData);
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

        //public HttpResponseParameter Request(HttpWebRequest request)
        //{
        //    string result = "";
        //    long elapsed = 0;
        //    string responseData = string.Empty;
        //    //Post请求地址
        //    Stopwatch stopWatch = Stopwatch.StartNew();
        //    try
        //    {
        //        HttpWebRequest m_Request = (HttpWebRequest)WebRequest.Create(requestUrl);
        //        //相应请求的参数
        //        byte[] data = Encoding.GetEncoding(codingType).GetBytes(requestData);
        //        m_Request.Method = "Post";
        //        m_Request.ContentType = contentType;
        //        m_Request.ContentLength = data.Length;
        //        m_Request.Timeout = timeout;
        //        m_Request.ServicePoint.Expect100Continue = false;
        //        //请求流
        //        Stream requestStream = m_Request.GetRequestStream();
        //        requestStream.Write(data, 0, data.Length);
        //        requestStream.Close();
        //        //响应流
        //        HttpWebResponse m_Response = (HttpWebResponse)m_Request.GetResponse();
        //        Stream responseStream = m_Response.GetResponseStream();
        //        StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(codingType));
        //        //获取返回的信息
        //        result = streamReader.ReadToEnd();
        //        streamReader.Close();
        //        responseStream.Close();
        //        stopWatch.Stop();
        //        elapsed = stopWatch.ElapsedMilliseconds;
        //    }
        //    catch (WebException ex)
        //    {
        //        stopWatch.Stop();
        //        elapsed = stopWatch.ElapsedMilliseconds;
        //        if (ex.Status == WebExceptionStatus.Timeout)
        //        {
        //            error = string.Format("请求超时[{0}],请求地址：{1}", elapsed, requestUrl);
        //        }
        //        else
        //        {
        //            error = string.Format("{0},请求地址：{1}", ex.Message, requestUrl);
        //        }
        //        result = error;
        //        responseData = ex.ToString();
        //        responseData += GetHTTPInternalServerErrorDetail(ex);
        //        RecordException(requestUrl, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        error = string.Format("请求接口异常,请求地址：{0}", requestUrl);
        //        result = error;
        //        elapsed = stopWatch.ElapsedMilliseconds;
        //        responseData = ex.ToString();
        //        RecordException(requestUrl, ex);
        //    }
        //    if (string.IsNullOrWhiteSpace(responseData))
        //    {
        //        responseData = result;
        //    }
        //    Logger.WriteHTTPLog(requestUrl, elapsed, requestData, responseData);
        //    return Toolkit.ClearSpecialCharForReq(result);
        //}

        /// <summary>
        /// HTTP REQUEST 请求 加密模式
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [Obsolete("无效方法")]
        public HttpResponseParameter Request(HttpRequestParameter para, bool isEncrypt)
        {
            HttpResponseParameter resPara = new HttpResponseParameter();

            #region 参数设置
            //game1-cbt.ma.sdo.com
            string host = "http://117.121.6.138:10001";
            string url = host + para.RequestUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(para.RequestData);
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
        [Obsolete("无效方法")]
        public HttpResponseParameter Request2(HttpRequestParameter reqPara)
        {
            HttpResponseParameter resPara = new HttpResponseParameter();
            byte[] byteSend = CommonSetting.httpEncode.GetBytes(reqPara.RequestData);
            string result = string.Empty;
            HttpWebResponse response = null;
            HttpResponseParameter res = new HttpResponseParameter();
            try
            {
                Stream streamSend = reqPara.request.GetRequestStream();
                streamSend.Write(byteSend, 0, byteSend.Length);
                streamSend.Close();
                //接收HTTP做出的响应  
                response = (HttpWebResponse)reqPara.request.GetResponse();
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
