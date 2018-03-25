using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyFast
{
    public class ColomnInfo
    {
        public string columnName { get; set; }

        public bool isIdAttribute { get; set; }

        public int Strategy { get; set; }

        public bool isUnique { get; set; }

        public PropertyInfo proptoty { get; set; }

        public bool addDot { get; set; }
    }
}
