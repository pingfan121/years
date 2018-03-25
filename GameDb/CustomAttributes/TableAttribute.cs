using System;
using System.Collections.Generic;
using System.Text;

namespace Easy4net.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        private string _Name = string.Empty;
        
        public TableAttribute() {
            NoAutomaticKey = false;
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 不具备自增长键的表
        /// </summary>
        public bool NoAutomaticKey { get; set; }
    }
}
