using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MillionSimple.CommonEnum;

namespace MillionSimple.Common
{
    public class AttributeHelper
    {
        public static string GetEnumDes(Type type, string value)
        {
            string ret = "未补充的卡";
            //获取所有的字段信息
            FieldInfo[] f = type.GetFields();

            //遍历每个字段；
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i].Name == "c" + value)
                {
                    var attr = f[i].GetCustomAttributes(false);
                    Name a = attr[0] as Name;
                    ret = a.Des;
                    break;
                }
            }
            return ret;
        }
    }
}
