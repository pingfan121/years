using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDb.Logic
{
    public class TbLogic
    {
        public TbLogic() { 
        }
        [JsonIgnore]
        public HashSet<string> changedKeys = new HashSet<string>();
        /// <summary>
        /// 从 t复制所有数据到自己身上
        /// </summary>
        /// <param name="t"></param>
        virtual public void copy(TbLogic t) {
            if (t == this)
            {
                return;
            }
        }
    }
}
