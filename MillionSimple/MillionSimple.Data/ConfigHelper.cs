using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MillionSimple.Common;

namespace MillionSimple.Data
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public static class ConfigHelper
    {
        private static string Path = @"\MillionSimple\MillionSimple.Data\ConfigHelper.cs";



        /// <summary>
        /// 获取主号名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMasterName()
        {
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            List<string> master = new List<string>();
            try
            {
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Master");
                if (nodeList != null && nodeList.Count > 0)
                {
                    foreach (XmlNode node in nodeList)
                    {
                        master.Add(node.InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetMasterName", ex.Message);
            }
            return master;
        }
    }
}
