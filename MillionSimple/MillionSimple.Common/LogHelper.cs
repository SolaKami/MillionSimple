using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Common
{
    public static class LogHelper
    {

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="path"></param>
        /// <param name="funcName"></param>
        /// <param name="msg"></param>
        public static void ErrorLog(string path, string funcName, string msg)
        {
            string log = string.Empty;
            log += DateTime.Now.ToString();
            log += CommonString.HX;
            log += path;
            log += CommonString.HX;
            log += funcName;
            log += CommonString.HX;
            log += msg;
            log += CommonString.HX;
            log += CommonString.HX;
            ErrorMessageLog.writeLog(log);
        }

        /// <summary>
        /// HTTP日志
        /// </summary>
        /// <param name="msg"></param>
        public static void HttpLog(string msg)
        {

        }
    }
}
