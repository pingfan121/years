using System;
using System.Collections.Generic;
using System.Text;
using Easy4net.Common;
using System.Data;
using Easy4net.DBUtility;

namespace Easy4net.Common
{
    public class ParamMap : Map
    {
        private bool isPage;
//         private int pageOffset;
//         private int pageLimit;
        private string orderFields;
        private bool isDesc = true;

        private List<IDbDataParameter> m_ParamList = new List<IDbDataParameter>();

        private ParamMap() { }

        public string OrderFields
        {
            get { return orderFields; }
            set { orderFields = value; }
        }

        public bool IsDesc
        {
            get { return isDesc; }
            set { isDesc = value; }
        }

        public static ParamMap newMap()
        {
            return new ParamMap();
        }

        public bool IsPage
        {
            get
            {
                return isPage;
            }
        }

        public int PageOffset
        {
            get
            {
                if (this.ContainsKey("pageIndex") && this.ContainsKey("pageSize"))
                {
                    int pageIndex = this.getInt("pageIndex");
                    int pageSize = this.getInt("pageSize");
                    if (pageIndex <= 0) pageIndex = 1;
                    if (pageSize <= 0) pageSize = 1;

                    return (pageIndex - 1) * pageSize;
                }

                return 0;
            }
        }

        public int PageLimit
        {
            get
            {
                if (this.ContainsKey("pageSize"))
                {
                    return this.getInt("pageSize");
                }

                return 0;
            }
        }

        public int getInt(string key)
        {
            var value = this[key];
            return Convert.ToInt32(value);
        }

        public String getString(string key)
        {
            var value = this[key];
            return Convert.ToString(value);
        }

        public Double toDouble(string key)
        {
            var value = this[key];
            return Convert.ToDouble(value);
        }

        public Int64 toLong(string key)
        {
            var value = this[key];
            return Convert.ToInt64(value);
        }

        public Decimal toDecimal(string key)
        {
            var value = this[key];
            return Convert.ToDecimal(value);
        }

        public DateTime toDateTime(string key)
        {
            var value = this[key];
            return Convert.ToDateTime(value);
        }

        public void setOrderFields(string orderFields, bool isDesc)
        {
            this.orderFields = orderFields;
            this.isDesc = isDesc;
        }


        /// <summary>
        /// 此方法已过时，请使用 setPageParamters方法分页
        /// </summary>
        /// <param name="pageIndex"></param>
        private void setPageIndex(int pageIndex)
        {
            this["pageIndex"] = pageIndex;
            setPages();
        }

        /// <summary>
        /// 此方法已过时，请使用 setPageParamters方法分页
        /// </summary>
        /// <param name="pageSize"></param>
        private void setPageSize(int pageSize)
        {
            this["pageSize"] = pageSize;
            setPages();
        }

        /// <summary>
        /// 分页参数设置
        /// </summary>
        /// <param name="page">第几页，从0开始</param>
        /// <param name="limit">每页最多显示几条数据</param>
        public void setPageParamters(int page, int limit)
        {
            this["pageIndex"] = page;
            this["pageSize"] = limit;
            setPages();
        }

        private void setPages()
        {
            if (this.ContainsKey("pageIndex") && this.ContainsKey("pageSize"))
            {
                this.isPage = true;
                if (AdoHelper.DbType == DatabaseType.MYSQL)
                {
                    this["offset"] = this.PageOffset;
                    this["limit"] = this.PageLimit;

                    this.Remove("pageIndex");
                    this.Remove("pageSize");

                    this.Add("offset", this.getInt("offset"));
                    this.Add("limit", this.getInt("limit"));
                }

                if (AdoHelper.DbType == DatabaseType.SQLSERVER)
                {
                    int pageIndex = this.getInt("pageIndex");
                    int pageSize = this.getInt("pageSize");
                    if (pageIndex <= 0) pageIndex = 1;
                    if (pageSize <= 0) pageSize = 1;

                    this["pageStart"] = (pageIndex - 1) * pageSize + 1;
                    this["pageEnd"] = pageIndex * pageSize;

                    this.Remove("pageIndex");
                    this.Remove("pageSize");

                    this.Add("pageStart", this.getInt("pageStart"));
                    this.Add("pageEnd", this.getInt("pageEnd"));
                }

                if (AdoHelper.DbType == DatabaseType.ACCESS)
                {
                    int pageIndex = this.getInt("pageIndex");
                    int pageSize = this.getInt("pageSize");

                    this["page_offset"] = pageIndex * pageSize;
                    this["page_limit"] = pageSize;

                    this.Remove("pageIndex");
                    this.Remove("pageSize");
                }
            }
        }

        public override void Add(object key, object value)
        {
            base.Put(key, value);
            IDbDataParameter param = DbFactory.CreateDbParameter(key.ToString(), value);
            m_ParamList.Add(param);
        }

        public override void Put(object key, object value)
        {
            this.Add(key, value);
        }

        public override void setParameter(string key, object value)
        {
            this.Add(key, value);
        }

        public IDbDataParameter[] toDbParameters()
        {
            return m_ParamList.ToArray();
//             int i = 0;
//             IDbDataParameter[] paramArr = DbFactory.CreateDbParameters(m_ParamList.Count);
//             foreach (IDbDataParameter dbParameter in m_ParamList)
//             {
//                 paramArr[i] = dbParameter;
//                 i++;
//             }
// 
//             return paramArr;
        }
    }
}
