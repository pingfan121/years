using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuoBiApi.huobi_api
{

    public class hb_data
    {
        public string status;
        public long ts;
        public Object data;
    }
    public class hb_symbols
    {
        public string base_currency;
        public string quote_currency;
        public int price_precision;
        public int amount_precision;
        public string symbol_partition;
    }

    //火币的k线数据
    public class hb_kline
    {
        public long id;
        public double amount;
        public long count;
        public double open;
        public double close;
        public double low;
        public double high;
        public double vol;
    }
}
