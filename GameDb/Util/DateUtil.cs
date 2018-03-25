using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib.Util
{
    public class DateUtil
    {
        static DateTime BaseTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

        public static DateTime FromUnixTime(int timeStamp)
        {
            //System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return BaseTime.AddSeconds(timeStamp);// +8 * 60 * 60;
        }
//         public static int ToUnixTime(DateTime datetime)
//         {
//             return (int)datetime.Subtract(BaseTime).TotalSeconds;
//         }
        public static long ToUnixTime2(DateTime datetime)
        {
            // return (long)datetime.Subtract(BaseTime).TotalMilliseconds;
            return (long)datetime.Subtract(BaseTime).TotalMilliseconds;
        }

        //把当前时间转换成
        public static long NowToToUnixTime2()
        {
            return ToUnixTime2(DateTime.Now);
        }
    }
}
