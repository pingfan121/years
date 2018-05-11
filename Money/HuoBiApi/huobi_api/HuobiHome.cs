using GameDb.Util;
using GameLib.Util;
using HuobiAPI;
using HuoBiApi.huobi_api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using Years.Model;

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
            //string a =server.getKLine("btcusdt");
            //Console.WriteLine(b);


            huobiEntities hbdb = new huobiEntities();

            DateTime time1 = new DateTime();

            List<hb_symbols> symbols = null;

            while (true)
            {
                //半小时取一次所有交易对

                try
                {
                    DateTime time2 = DateTime.Now;

                    if ((time2 - time1).TotalMinutes > 30)
                    {
                        symbols = server.getAllSymbol();
                        time1 = time2;
                    }

                    if (symbols != null)
                    {
                        //去取数据

                        foreach (var item in symbols)
                        {
                            if (item.symbol_partition == "main" || item.symbol_partition == "innovation")
                            {
                                string symbol = item.base_currency + item.quote_currency;

                                List<hb_kline> data = server.getKLine(symbol,"1day",1);

                                if (data != null && data.Count > 0)
                                {
                                    hb_kline linedata = data[0];

                                    //存数据库....
                                    DateTime time3 = DateTime.Now;
                                    DateTime time4 = time3.AddDays(1);



                                    market m = hbdb.market.FirstOrDefault(a => (a.symbols == symbol && a.last_time >= time3.Date && a.last_time < time4.Date));
                                    
                                    if (m == null)
                                    {
                                        m = new market();

                                        m.symbols = symbol;
                                        m.coin_type = item.quote_currency;
                                        m.token_type = item.base_currency;
                                        m.open_price = linedata.open;
                                        m.close_price = linedata.close;
                                        m.rose = 0;
                                        m.turnover = 0;
                                        m.last_time = time3;

                                        hbdb.market.Add(m);
                                    }
                                    else
                                    {
                                        m.open_price = linedata.open;
                                        m.close_price = linedata.close;
                                        m.rose = 0;
                                        m.turnover = 0;
                                        m.last_time = time3;

                                        hbdb.Entry(m).State = EntityState.Modified;
                                    }

                                

                                    hbdb.SaveChanges();

                                    Thread.Sleep(100);

                                }

                            }
                        }
                    }

                        
                }
                catch(Exception ex)
                {
                    Log.error(ex);
                }

                Thread.Sleep(5000);


            }



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