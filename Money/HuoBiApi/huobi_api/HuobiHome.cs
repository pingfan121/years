using HuobiAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Money.huobi_api
{
    public class HuobiHome
    {


        public static void init()
        {
            //新建发送线程
            Thread readthread = new Thread(SendBackMsg);
            readthread.IsBackground = true;
            readthread.Start();
        }

        static HuobiService server = new HuobiService();

        private static void SendBackMsg()
        {
            //发往后台的数据
            //有需要重置的数据重置一下
            string a =server.getKLine("btcusdt");

            Console.WriteLine(a);

            //while (true)
            //{

            //    try
            //    {

            //        if (back_data.TryDequeue(out msg))
            //        {
            //            //发送消息
            //            GameHttp.ReturnData(msg);
            //        }
            //        else
            //        {
            //            Thread.Sleep(1);
            //        }

            //    }
            //    catch (Exception e)
            //    {
            //        log.error("后台消息处理异常", e);
            //    }


            //    Thread.Sleep(100);
            //}
        }
    }
}