//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Years.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class market
    {
        public int id { get; set; }
        public string symbols { get; set; }
        public string coin_type { get; set; }
        public string token_type { get; set; }
        public double open_price { get; set; }
        public double close_price { get; set; }
        public double rose { get; set; }
        public int turnover { get; set; }
        public System.DateTime last_time { get; set; }
    }
}
