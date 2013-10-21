using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Common
{
    public static class ToolKit
    {
        public static DateTime TimeStampToTimeString(string timeStamp)
        {
            DateTime conTime = new DateTime();
            DateTime firstTime = new DateTime(1970, 1, 1);
            int stime = -1;
            if (int.TryParse(timeStamp, out stime) == false)
            {
                conTime = Convert.ToDateTime(timeStamp);
            }
            else
            {
                conTime = Convert.ToDateTime(firstTime.AddSeconds(stime));
            }
            return conTime;
        }

        public static int StringToInt(string value)
        {
            int ret = 0;
            try
            {
                ret = Convert.ToInt32(value);
            }
            catch (Exception)
            {
            }
            return ret;
        }
    }
}
