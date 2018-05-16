using HuoBiApi.huobi_api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.Model;

namespace HuoBiApi.Bll
{
    public class HomeBll
    {
        public static List<market> getSymbolsList(DateTime time)
        {
            using (huobiEntities hbdb = new huobiEntities())
            {

                var st = time.Date;
                var st2 = st.AddDays(1);

                var m = hbdb.market.Where(a => (a.last_time >=st && a.last_time<st2));

                return m.ToList() ;
            }
        }
    }
}
