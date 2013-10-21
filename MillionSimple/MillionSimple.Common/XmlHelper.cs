using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MillionSimple.Common
{
    public static class XmlHelper
    {
        public static string Path = @"\MillionSimple\MillionSimple.Common\XmlHelper.cs";

        public static string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\AppConfig.xml";

        public static string ReadXml(string xml, string xpath)
        {
            if (string.IsNullOrEmpty(xpath) || string.IsNullOrEmpty(xml))
            {
                return "";
            }
            string ret = string.Empty;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                ret = xmlDoc.SelectSingleNode(xpath).InnerText;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "ReadXml", ex.Message);
            }
            return ret;
        }

        public static XmlDocument GetXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetXml", ex.Message);
            }
            return xmlDoc;
        }

        #region 获取当前配置XML
        /// <summary>
        /// 获取当前配置XML
        /// </summary>
        /// <returns></returns>
        public static XmlDocument GetConfig()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(ConfigPath);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetConfig", ex.Message);
            }
            return xmlDoc;
        }
        #endregion

    }
}
