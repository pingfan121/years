using GameDb.Util;
using GameLib.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UtilLib
{
    public class CommonDataGet
    {
        public static void init()
        {
            {
                //获取美元兑人民币的汇率

                //设置定时间隔(毫秒为单位)
                int interval = 1000 * 60*30;
                Timer timer = new Timer(interval);
                //设置执行一次（false）还是一直执行(true)
                timer.AutoReset = true;
                //设置是否执行System.Timers.Timer.Elapsed事件
                timer.Enabled = true;
                //绑定Elapsed事件
                timer.Elapsed += getUSRMBRate;

                getUSRMBRate(null, null);
            }
          
        }
        //汇率来源  NOWAPI
        private static void getUSRMBRate(object sender, ElapsedEventArgs e)
        {
            try
            {
                WebClient wb = new WebClient();

                wb.Headers.Clear();
                wb.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                string app_key = "33719";
                string sign = "398dd88f2dc691feb99590342d9e5c21";

                wb.Encoding = Encoding.UTF8;

                string data = wb.DownloadString(String.Format("http://api.k780.com/?app=finance.rate&scur=USD&tcur=CNY&appkey={0}&sign={1}&format=json", app_key, sign));

                object bb = JSON.Decode<Hashtable>(data)["result"];

                string aa = JSON.Encode(JSON.Decode<Hashtable>(data)["result"]);



                CommonData.setUSRMBrate(double.Parse(JSON.Decode < Hashtable >(aa)["rate"].ToString()));
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }


        }
    }
}
