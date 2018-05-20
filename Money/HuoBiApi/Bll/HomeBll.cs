using HuoBiApi.huobi_api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilLib;
using Years.Model;
using AutoMapper;

namespace HuoBiApi.Bll
{
    public class HomeBll
    {
        public static List<hb_market> getSymbolsList(string type,string time)
        {
            using (huobiEntities hbdb = new huobiEntities())
            {
                DateTime time1 = DateTime.Parse(time);

                var st = time1.Date;
                var st2 = st.AddDays(1);

                double rate = CommonData.getUSRMBrate();

                List<market> m_s = null;


                if(type=="all")
                {
                    var m = hbdb.market.Where(a => (a.last_time >= st && a.last_time < st2)).OrderByDescending(a=>a.rose);

                    m_s = m.ToList();
                }
                else
                {
                    var m = hbdb.market.Where(a => ( a.token_type == type && a.last_time >= st && a.last_time < st2)).OrderByDescending(a => a.rose);

                    m_s = m.ToList();
                }

                market m_btc= hbdb.market.FirstOrDefault(a => (a.coin_type == "btc" && a.token_type == "usdt" && a.last_time >= st && a.last_time < st2));

                market m_eth = hbdb.market.FirstOrDefault(a => (a.coin_type == "eth" && a.token_type == "usdt" && a.last_time >= st && a.last_time < st2));

                List<hb_market> markets = new List<hb_market>();

                //AutoMapper自动映射
                

                foreach (var item in m_s)
                {
                    hb_market hb_m = Mapper.Map<market, hb_market>(item);

                    if(item.token_type=="usdt")
                    {
                        hb_m.rate = rate;
                    }
                    else if (item.token_type == "btc")
                    {
                        hb_m.rate = m_btc.close_price * rate;
                    }
                    else if (item.token_type == "eth")
                    {
                        hb_m.rate = m_eth.close_price * rate;
                    }

                    markets.Add(hb_m);
                }

                return markets;


            }
        }


        public static List<market> getGoodSymbols(string type)
        {
            using (huobiEntities hbdb = new huobiEntities())
            {
                DateTime time1 = DateTime.Now;

                var st = time1.Date;
                var st2 = st.AddDays(1);
                if (type == "all")
                {
                    var m = hbdb.market.Where(a => (a.last_time >= st && a.last_time < st2));

                    return m.ToList();
                }
                else
                {
                    var m = hbdb.market.Where(a => (a.token_type == type && a.last_time >= st && a.last_time < st2));

                    return m.ToList();
                }

            }
        }
    }
}
