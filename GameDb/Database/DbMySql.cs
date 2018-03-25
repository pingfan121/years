
/********************************************************************
	创建日期:	2014/12/09
	创建时间:	9:12:2014   16:24
	文件名:		DbConnect
	文件类型:	cs
	作者:		zhangshaohua
	
	描述:		
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy4net.Session;
using Easy4net.DBUtility;
using System.Collections;
using Easy4net.Common;
using GameDb.Logic;
using System.Data;

namespace GameLib.Database
{
    public class DbMySql : IDbConnect
    {
        private string _connectString;
        private Session _poolSession;

        public DbMySql()
        {
            _connectString = "";
            _poolSession = new Session();
        }

        public bool Connected {
            get
            {
                return _poolSession.State== ConnectionState.Open; ;
            }
        }
        public int init(DbConfig config)
        {
            if (_connectString != "")
                return -1;

            StringBuilder sb = new StringBuilder();

            AdoHelper.SetDbType("mysql");

            sb.Append("server=").Append(config.Host).Append(";");

            if (!String.IsNullOrEmpty(config.Port))
            {
                sb.Append("port=").Append(config.Port).Append(";");
            }

            sb.Append("User ID=").Append(config.User).Append(";");
            sb.Append("Password=").Append(config.Pwd).Append(";");
            sb.Append("DataBase=").Append(config.Name).Append(";");

            if (!String.IsNullOrEmpty(config.MinPoolSize))
            {
                sb.Append("Min Pool Size=").Append(config.MinPoolSize).Append(";");
            }

            if (!String.IsNullOrEmpty(config.MinPoolSize))
            {
                sb.Append("Max Pool Size=").Append(config.MaxPoolSize).Append(";");
            }

            if (!String.IsNullOrEmpty(config.CharSet))
            {
                sb.Append("charset=").Append(config.CharSet).Append(";");
            }

            sb.Append("Convert Zero Datetime=True;");
            sb.Append("allow zero datetime=true;");

            _connectString = sb.ToString();
            _poolSession.ConnectionString = _connectString;
            
            return 0;
        }
        public void release()
        {
        }

        public long getNextSequence<T>()
        {
            return 0;
        }

        public List<T> find<T>(string query) where T : TbLogic,new()
        {
            Session ses = _poolSession;
            List<T> v = null;
            v = ses.Find<T>(query);

            return v;
        }

        public int insert<T>(T t) where T : TbLogic
        {
            Session ses = _poolSession;
            int v = 0;
            v = ses.Insert<T>(t);

            return v;
        }

        public int update<T>(T t) where T : TbLogic
        {
            Session ses = _poolSession;
            int v = 0;

            v = ses.Update<T>(t);

            return v;
        }

        public int delete<T>(T t) where T : TbLogic
        {
            Session ses = _poolSession;
            int v = 0;

            v = ses.Delete<T>(t);

            return v;
        }

        public int insert<T>(List<T> listT) where T : TbLogic
        {
            Session ses = _poolSession;
            int v = 0;

            v = ses.Insert<T>(listT);


            return v;
        }

        public int update<T>(List<T> listT) where T : TbLogic{
            Session ses = _poolSession;
            int v = 0;

            v = ses.Update<T>(listT);
            return v;
        }

        public int delete<T>(List<T> listT) where T : TbLogic
        {
            Session ses = _poolSession;
            int v = 0;

            v = ses.Delete<T>(listT);

            return 0;
        }

        public int executeNonQuery(string sql, Hashtable param)
        {
            if (sql == null || sql == "")
                return -1;

            ParamMap pm = null;
            if (param != null && param.Count > 0)
            {
                pm = ParamMap.newMap();
                foreach (DictionaryEntry v in param)
                    pm.Add(v.Key, v.Value);
            }

            Session ses = _poolSession;

            if (pm == null)
                ses.ExcuteSQL(sql);
            else
                ses.ExcuteSQL(sql, pm);

            return 0;
        }

    }
}