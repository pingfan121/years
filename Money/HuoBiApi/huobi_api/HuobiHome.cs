using AutoMapper;
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

            initmap();
        }

        private static void initmap()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<market, hb_market>());
        }

        static HuobiService server = new HuobiService();

        private static void SendBackMsg()
        {
           
         //   huobiEntities hbdb = new huobiEntities();

            DateTime time1 = new DateTime();

            List<hb_symbols> symbols = null;

            int count = 150;

            Log.error("测试日志");

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
                        using (huobiEntities hbdb = new huobiEntities())
                        {

                        
                            foreach (var item in symbols)
                            {
                                if (item.symbol_partition == "main" || item.symbol_partition == "innovation")
                                {
                                    string symbol = item.base_currency + item.quote_currency;

                                    List<hb_kline> data = server.getKLine(symbol, "1day", count);

                                    //存数据库....
                                    DateTime time3 = DateTime.Now;

                                    DateTime time4 = time3.Date.AddHours(23).AddMinutes(59);

                                    if (data != null && data.Count > 0)
                                    {

                                        for (var i = 0; i < data.Count; i++)
                                        {
                                            hb_kline linedata = data[i];

                                            DateTime temptime = time4.AddDays(-i).Date;
                                            DateTime temptime2 = temptime.Date.AddDays(1);

                                            market m = hbdb.market.FirstOrDefault(a => (a.symbols == symbol && a.last_time >= temptime && a.last_time < temptime2));

                                            if (m == null)
                                            {
                                                m = new market();

                                                m.symbols = symbol;
                                                m.coin_type = item.base_currency;
                                                m.token_type = item.quote_currency;
                                                m.open_price = linedata.open;
                                                m.close_price = linedata.close;
                                                m.rose = m.open_price != 0 ? (m.close_price - m.open_price) / m.open_price : 0;
                                                m.turnover = 0;
                                                m.last_time = i == 0 ? time3 : time4.AddDays(-i);

                                                hbdb.market.Add(m);
                                            }
                                            else
                                            {
                                                m.open_price = linedata.open;
                                                m.close_price = linedata.close;
                                                m.rose = m.open_price != 0 ? (m.close_price - m.open_price) / m.open_price : 0;
                                                m.turnover = 0;
                                                m.last_time = i == 0 ? time3 : time4.AddDays(-i);

                                                hbdb.Entry(m).State = EntityState.Modified;
                                            }
                                        }

                                        hbdb.SaveChanges();

                                        Thread.Sleep(100);

                                    }
                                }
                            }
                        }
                        count = 1;
                    }

                        
                }
                catch(Exception ex)
                {
                    Log.error(ex);
                }

                Thread.Sleep(5000);


            }
        }


    }
}