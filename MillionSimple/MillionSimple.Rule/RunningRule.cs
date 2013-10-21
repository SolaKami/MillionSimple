using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Common;
using System.Data;
using MillionSimple.Data;
using System.Text.RegularExpressions;
using System.Xml;
using MillionSimple.Model;
using MillionSimple.CommonEnum;
namespace MillionSimple.Rule
{
    /// <summary>
    /// 自动运行类
    /// </summary>
    public class RunningRule
    {
        #region 构造函数

        #endregion

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

        #region 方法

        #region 游戏操作动作接口
        /// <summary>
        ///游戏操作动作接口
        /// </summary>
        /// <param name="actModeEnum"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public HttpResponseParameter DoAct(HttpRequestParameter para, bool isEncrypt = false)
        {
            HttpResponseParameter respara;
            if (isEncrypt)
            {
                respara = Http.Request(para, true);
            }
            else
            {
                respara = Http.Request(para);
            }
            respara.ErrorCode = Analyze.GetCommandCode(respara.Content);
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
                    content += string.Format("指令:{0}", para.Act.ToString()) + "\r\n";
                    content += respara.Content;
                    content += "\r\n\r\n";
                    HttpMessageLog.writeLog(content);
                }
                else
                {
                    string content = string.Empty;
                    content += Des.GetTimeDes() + "\r\n";
                    content += string.Format("指令:{0}", para.Act.ToString()) + "\r\n";
                    content += respara.ErrorMsg;
                    content += "\r\n\r\n";
                    HttpMessageLog.writeLog(content);
                }
            }

            #endregion
            return respara;
        }
        #endregion

        #endregion

        /// <summary>
        /// MAkong
        /// </summary>
        /// <param name="reqPara"></param>
        /// <returns></returns>
        public HttpResponseParameter GetFootBall(HttpRequestParameter reqPara)
        {
            HttpResponseParameter respara = Http.Request2(reqPara);
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
                    content += string.Format("指令:足球队查询") + "\r\n";
                    content += respara.Content;
                    content += "\r\n\r\n";
                    HttpMessageLog.writeLog(content);
                }
                else
                {
                    string content = string.Empty;
                    content += Des.GetTimeDes() + "\r\n";
                    content += string.Format("指令:足球队查询") + "\r\n";
                    content += respara.ErrorMsg;
                    content += "\r\n\r\n";
                    HttpMessageLog.writeLog(content);
                }
            }

            #endregion
            return respara;
        }
    }


}
