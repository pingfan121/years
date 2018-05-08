using System;

namespace GameLib.Util
{
    public class DateUtil
    {
        static DateTime BaseTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        static DateTime BaseTime1 = new DateTime(1970, 1, 1);

        public static DateTime FromUnixTime(int timeStamp)
        {
            //System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return BaseTime.AddSeconds(timeStamp);// +8 * 60 * 60;
        }
        public static int ToUnixTime(DateTime datetime)
        {
            return (int)datetime.Subtract(BaseTime).TotalSeconds;
        }
        public static long ToUnixTime2(DateTime datetime)
        {
            return (long)datetime.Subtract(BaseTime).TotalMilliseconds;
        }
        public static int TimeZoneDifferenceTime()
        {
            return (int)BaseTime.Subtract(BaseTime1).TotalSeconds;
        }
        public static long TimeZoneDifferenceTime2()
        {
            return (long)BaseTime.Subtract(BaseTime1).TotalMilliseconds;
        }
    }
}
