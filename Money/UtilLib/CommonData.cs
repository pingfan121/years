using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilLib
{
    //这是一个静态通用数据类 所定义的字段都必须是 int 类型的  线程操作原子是int 所以不用考虑跨线程的问题
    public class CommonData
    {

        private static object locker=new object();

        public static double USRMB_rate;//美元对人民币汇率


        

        public static void setUSRMBrate(double rate)
        {
            lock(locker)
            {
                USRMB_rate = rate;
            }
        }

        public static double getUSRMBrate()
        {
            lock (locker)
            {
                return USRMB_rate;
            }
        }
    }
}