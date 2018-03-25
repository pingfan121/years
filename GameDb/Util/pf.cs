using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hudie.tool
{
    public class pf
    {
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        } 

        //得到时间
        
        public static  DateTime GetTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        //验证电话号码的主要代码如下：
        public static bool IsMobile(string num)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(num, @"^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
        }
    }
}
