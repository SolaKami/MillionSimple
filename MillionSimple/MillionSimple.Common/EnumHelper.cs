using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace MillionSimple.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(this object obj, bool isTop)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                Type _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                if (isTop)
                {
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(_enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                       fi, typeof(DescriptionAttribute));
                }
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {
            }
            return obj.ToString();
        }

        public static string GetEnumDes(Type type, string value)
        {
            string ret = string.Empty;
            FieldInfo[] f = type.GetFields();
            //遍历每个字段；
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i].Name == "c" + value)
                {
                    var attr = f[i].GetCustomAttributes(false);
                    DescriptionAttribute a = attr[0] as DescriptionAttribute;
                    ret = a.Description;
                    break;
                }
            }
            return ret;
        }
    }

}
