using Easy4net.Common;
using Easy4net.CustomAttributes;
using Easy4net.DBUtility;
using EasyFast;
using GameDb.Logic;
using GameDb.Util;
using GameLib.Util;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Easy4net.Session
{
    public class TableInfo
    {
        public string TableName;
        //<字段名,字段信息>
        public Dictionary<string, ColomnInfo> colomns = new Dictionary<string, ColomnInfo>();
        public ColomnInfo Id;//主键
    }
    public class Session
    {
        /// <summary>
        /// 获取数据表名
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        static public string GetTableName(Type classType)
        {
            string strTableName = string.Empty;
            string strEntityName = string.Empty;

            strEntityName = classType.FullName;

            object[] attr = classType.GetCustomAttributes(false);
            if (attr.Length == 0) return strTableName;

            foreach (object classAttr in attr)
            {
                if (classAttr is TableAttribute)
                {
                    TableAttribute tableAttr = classAttr as TableAttribute;
                    strTableName = tableAttr.Name;
                }
            }
            if (string.IsNullOrEmpty(strTableName))
            {
                throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
            }
            return strTableName;
        }
        static public Dictionary<Type, TableInfo> dics = new Dictionary<Type, TableInfo>();
        /// <summary>
        /// 获取列属性
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetColumnName(object attribute)
        {
            string columnName = string.Empty;
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                columnName = columnAttr.Name;
            }
            else if (attribute is IdAttribute)
            {
                IdAttribute idAttr = attribute as IdAttribute;
                columnName = idAttr.Name;
            }

            return columnName;
        }
        static private TableInfo getInfo(Type type)
        {
            if (!dics.ContainsKey(type))
            {
                TableInfo temp = new TableInfo();
                temp.TableName = GetTableName(type);
                PropertyInfo[] propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in propertys)
                {
                    object[] propertyAttrs = property.GetCustomAttributes(false);
                    for (int i = 0; i < propertyAttrs.Length; i++)
                    {
                        object propertyAttr = propertyAttrs[i];

                        string tempVal = GetColumnName(propertyAttr);//返回数据的字段名
                        if (tempVal == string.Empty)
                        { //这个字段不需要进行处理

                        }
                        else
                        {
                            ColomnInfo cinfo = new ColomnInfo();
                            cinfo.columnName = tempVal;
                            cinfo.proptoty = property;

                            string typeName = TypeUtils.GetTypeName(property.PropertyType);
                            if (typeName == "System.String" || typeName == "System.DateTime")
                            {
                                cinfo.addDot = true;
                            }
                            if (propertyAttr is IdAttribute)
                            {
                                cinfo.isIdAttribute = true;
                                cinfo.isUnique = true;
                                cinfo.Strategy = (propertyAttr as IdAttribute).Strategy;
                                temp.Id = cinfo;
                            }
                            else if (propertyAttr is ColumnAttribute)
                            {
                                cinfo.isUnique = (propertyAttr as ColumnAttribute).IsUnique;
                            }
                            temp.colomns[property.Name] = cinfo;
                        }
                    }
                }
                dics[type] = temp;
            }
            return dics[type];
        }
        /// <summary>
        /// 
        /// </summary>
        private string _connectionString;
        private IDbConnection _connect;

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                Open();
            }
        }

        public Session()
        {
            _connectionString = null;
            _connect = null;
        }
        private ConnectionState state;
        public ConnectionState State
        {
            get { return state; }
        }
        public void Open()
        {
            if (_connect == null)
            {
                DBLOG.log(_connectionString);
                _connect = DbFactory.CreateDbConnection(_connectionString);
                (_connect as MySqlConnection).StateChange += conn_StateChange;
                _connect.Open();
            }
            else if (state == ConnectionState.Closed)
            {
                _connect.Open();
            }
            else if (state == ConnectionState.Broken)
            {
                _connect.Close();
                _connect.Open();
            }
        }
        //数据库状态发生改变处理事件
        void conn_StateChange(object sender, StateChangeEventArgs e)
        {
            state = e.CurrentState;

            DBLOG.warn("Session数据库>>>>新状态=" + e.CurrentState);

            if (e.CurrentState == ConnectionState.Closed)
            {
                
                _connect.Open();
                Thread.Sleep(1000);
            }
            else if (e.CurrentState == ConnectionState.Broken)
            {
                Thread.Sleep(1000);
                _connect.Close();
                _connect.Open();
            }
        }
        private IDbConnection GetConnection()
        {
            return _connect;
        }
        #region 将实体数据保存到数据库
        public int Insert<T>(T ent)
        {
            if (ent == null) return 0;

            TableInfo dic = getInfo(typeof(T));
            string sql = "insert into " + dic.TableName + "({0}) VALUES({1})";
            StringBuilder a = new StringBuilder();
            StringBuilder b = new StringBuilder();
            int c = dic.colomns.Count;
            MySqlCommand cmd = new MySqlCommand();
            foreach (ColomnInfo co in dic.colomns.Values)
            {
                if (--c > 0)
                {
                    a.Append(co.columnName).Append(",");
                    if (co.addDot)
                    {
                        b.Append("?").Append(co.columnName).Append(",");
                        cmd.Parameters.Add(new MySqlParameter("?" + co.columnName, co.proptoty.GetValue(ent, null)));
                        //b.Append("'").Append(co.proptoty.GetValue(ent, null)).Append("',");
                    }
                    else
                    {
                        b.Append(co.proptoty.GetValue(ent, null)).Append(",");
                    }
                }
                else
                {
                    a.Append(co.columnName);
                    
                    
                    if (co.addDot)
                    {
                        b.Append("?").Append(co.columnName);
                        cmd.Parameters.Add(new MySqlParameter("?" + co.columnName, co.proptoty.GetValue(ent, null)));
                        //b.Append("'").Append(co.proptoty.GetValue(ent, null)).Append("'");
                    }
                    else
                    {
                        b.Append(co.proptoty.GetValue(ent, null));
                    }
                }
            }
            sql = string.Format(sql, a, b);
            cmd.CommandText = sql;


            return Execute(cmd);
        }
        #endregion

        private int Execute(MySqlCommand cmd)
        {
            DBLOG.log(cmd.CommandText);
            IDbConnection conn = this.GetConnection();
            cmd.Connection = conn as MySqlConnection;
            
            int re = 0;
            try
            {
                re = cmd.ExecuteNonQuery();
                /*
                StringBuilder str = new StringBuilder();
                //cmd.Parameters.RemoveAt("item");
                foreach (MySqlParameter o in cmd.Parameters)
                {
                    str.Append(o.ParameterName).Append("=").Append(o.Value.ToString()).Append("&");
                }
                DBLOG.log(str.ToString());*/
            }
            catch (MySqlException mysqle)
            {
                DBLOG.error("出错3:",mysqle);// + JSON.Encode(cmd.Parameters),mysqle);
                
                StringBuilder str = new StringBuilder();
                //cmd.Parameters.RemoveAt("item");
                foreach(MySqlParameter o in cmd.Parameters){
                    str.Append(o.ParameterName).Append("=").Append(o.Value.ToString());
                }
                DBLOG.error(str.ToString());
            }
            catch (System.IO.IOException ioe)
            {
                DBLOG.error("出错4" + ioe.Message,ioe);
                conn.Close();
            }
            return re;
        }
        private int Execute(string sql)
        {
            DBLOG.log("Execute:"+sql);
            IDbConnection conn = this.GetConnection();
            int re = 0;
            MySqlCommand cmd = new MySqlCommand(sql, (MySqlConnection)conn);
            try
            {
                re = cmd.ExecuteNonQuery();
            }
            catch (MySqlException mysqle)
            {
                DBLOG.error("出错3:", mysqle);
            }
            catch (System.IO.IOException ioe)
            {
                DBLOG.error("出错4", ioe);
                conn.Close();
            }

            return re;
        }
        public static HashSet<Type> dontneedYouhua = new HashSet<Type>();

        #region 批量保存
        public int Insert<T>(List<T> entityList)
        {
            if (entityList == null || entityList.Count == 0) return 0;

            int val = 0;

            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                foreach(T t in entityList){
                    val+=Insert<T>(t);
                }    
            }
            catch (Exception e)
            {
                DBLOG.error("Insert=" + entityList, e);
            }

            return val;
        }
        #endregion
        static public bool saveall = false;
        #region 将实体数据修改到数据库
        public int Update<T>(T ent) where T : TbLogic
        {

            TableInfo dic = getInfo(typeof(T));
            //移除id，确保不会改变
            int c = ent.changedKeys.Count;
            if (c == 0)
            {
                return 1;//其实无需更新
            }
            MySqlCommand cmd = new MySqlCommand();
            //
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(dic.TableName).Append(" set ");
            //id不允许改变

            string where = " where ";
            if (dic.Id == null)
            {
                throw new Exception(ent + "必须指定主键");
            }
            else
            {
                if (dic.Id.addDot)
                {
                    where += dic.Id.columnName + "='" + dic.Id.proptoty.GetValue(ent, null) + "'";
                }
                else
                {
                    where += dic.Id.columnName + "=" + dic.Id.proptoty.GetValue(ent, null);
                }
            }

            foreach (string key in ent.changedKeys)
            {
                ColomnInfo co = dic.colomns[key];
                if (--c > 0)
                {
                    if (co.isIdAttribute)
                    {
                        if (co.addDot)
                        {
                            where += co.columnName + "='" + co.proptoty.GetValue(ent, null) + "'";
                        }
                        else
                        {
                            where += co.columnName + "=" + co.proptoty.GetValue(ent, null);
                        }
                    }
                    else
                    {
                        if (co.addDot)
                        {
                            //sql.Append(co.columnName).Append("='").Append(co.proptoty.GetValue(ent, null)).Append("',");
                            cmd.Parameters.Add(new MySqlParameter("?" + co.columnName, co.proptoty.GetValue(ent, null)));
                            sql.Append(co.columnName).Append("=?").Append(co.columnName).Append(",");
                        }
                        else
                        {
                            sql.Append(co.columnName).Append("=").Append(co.proptoty.GetValue(ent, null)).Append(",");
                        }
                    }
                }
                else
                {
                    if (co.addDot)
                    {
                        //sql.Append(co.columnName).Append("='").Append(co.proptoty.GetValue(ent, null)).Append("'");
                        cmd.Parameters.Add(new MySqlParameter("?" + co.columnName, co.proptoty.GetValue(ent, null)));
                        sql.Append(co.columnName).Append("=?").Append(co.columnName);
                    }
                    else
                    {
                        sql.Append(co.columnName).Append("=").Append(co.proptoty.GetValue(ent, null));
                    }
                }
            }
            sql.Append(where);

            //
            cmd.CommandText = sql.ToString();

            int re = Execute(cmd);
            if (dontneedYouhua.Contains(typeof(T)) || saveall)
            {
            }
            else {
                //不优化
                ent.changedKeys.Clear();
            }
            return re;
        }
        #endregion

        #region 批量更新
        public int Update<T>(List<T> entityList) where T : TbLogic
        {
            if (entityList == null || entityList.Count == 0) return 0;

            int val = 0;
            foreach(T t in entityList){
                val+=Update<T>(t);
            }
            return val;
        }
        #endregion

        #region 执行SQL语句
        public int ExcuteSQL(string strSQL, ParamMap param)
        {
            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                {
                    connection = GetConnection();

                    IDbDataParameter[] parms = param.toDbParameters();

                    if (AdoHelper.DbType == DatabaseType.ACCESS)
                    {
                        strSQL = SQLBuilderHelper.builderAccessSQL(strSQL, parms);
                        val = AdoHelper.ExecuteNonQuery(connection, transaction, CommandType.Text, strSQL);
                    }
                    else
                    {
                        val = AdoHelper.ExecuteNonQuery(connection, transaction, CommandType.Text, strSQL, parms);
                    }
                }
            }
            catch (Exception e)
            {
                DBLOG.error(strSQL, e);
            }
            return Convert.ToInt32(val);
        }
        #endregion

        #region 执行SQL语句
        public int ExcuteSQL(string strSQL)
        {
            int val = 0;
            DBLOG.log("ExcuteSQL>" + strSQL);
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                val = new MySqlCommand(strSQL,connection as MySqlConnection).ExecuteNonQuery();// AdoHelper.ExecuteNonQuery(connection, null, CommandType.Text, strSQL);
                
            }
            catch (MySqlException mysqle)
            {
                DBLOG.error("出错ExcuteSQL:", mysqle);
            }
            catch (System.IO.IOException ioe)
            {
                DBLOG.error("出错5", ioe);
                connection.Close();
            }
            return val;
        }
        #endregion

        #region 删除实体对应数据库中的数据
        public int Delete<T>(T entity)
        {
            if (entity == null) return 0;

            int val = 0;
            TableInfo info = getInfo(typeof(T));
            string sql = "DELETE FROM Persons WHERE " + info.Id.columnName + "='{0}'";
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                {
                    connection = GetConnection();
                    string Sql = string.Format(sql, info.Id.proptoty.GetValue(entity, null));
                    val = Execute(Sql);
                    //to do
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return val;
        }
        #endregion

        #region 批量删除
        public int Delete<T>(List<T> entityList)
        {
            if (entityList == null || entityList.Count == 0) return 0;

            int val = 0;
            TableInfo info = getInfo(typeof(T));
            StringBuilder sb = new StringBuilder();
            string sql = "DELETE FROM Persons WHERE " + info.Id.columnName + "='";// ++"'";
            IDbConnection connection = null;
            try
            {
               
                TableInfo tableInfo = getInfo(typeof(T));
                //获取数据库连接
                connection = GetConnection();
                {
                    connection = GetConnection();
                    foreach (T t in entityList) { 
                        sb.Append(sql).Append(info.Id.proptoty.GetValue(t,null).ToString()).Append("'\";");//一条 删除记录
                    }
                }
                val = Execute(sb.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
            return val;
        }
        #endregion

        #region 通过自定义SQL语句查询记录数
        public int Count(string strSQL)
        {
            int count = 0;
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                {
                    connection = GetConnection();
                    count = Execute(strSQL);
                }
            }
            catch (Exception ex)
            {
                DBLOG.error(strSQL, ex);
            }

            return count;
        }
        #endregion

        #region 通过自定义SQL语句查询记录数
        public int Count(string strSql, ParamMap param)
        {
            throw new Exception("暂时未支持");
            /*int count = 0;
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                strSql = strSql.ToLower();
                String columns = SQLBuilderHelper.fetchColumns(strSql);
                count = Convert.ToInt32(AdoHelper.ExecuteScalar(connection, CommandType.Text, strSql, param.toDbParameters()));
            }
            catch (Exception ex)
            {
                DBLOG.error(strSql, ex);
            }

            return count;*/
        }
        #endregion

        #region 通过自定义SQL语句查询数据
        public List<T> Find<T>(string sql) where T : TbLogic,new()
        {
            List<T> list = new List<T>();
            MySqlCommand cmd = new MySqlCommand(sql, (MySqlConnection)GetConnection());
            DBLOG.log(sql);
            TableInfo dic = getInfo(typeof(T));
            MySqlDataReader sdr = cmd.ExecuteReader();
            try
            {
                while (sdr.Read())
                {

                    T entity = new T();
                    foreach (ColomnInfo co in dic.colomns.Values)
                    {
                        String name = co.columnName;
                        if(CheckColumnName(sdr,name)==true)
                        {
                            if(sdr[name] is MySqlDateTime)
                            {
                                co.proptoty.SetValue(entity, ((MySqlDateTime)sdr[name]).GetDateTime(), null);
                            }
                            else
                            {
                                co.proptoty.SetValue(entity, sdr[name], null);
                            }
                        }
                      
                    }
                    entity.changedKeys.Clear();//清理
                    list.Add(entity);
                }
            }
            finally
            {
                if (!sdr.IsClosed)
                {
                    sdr.Close();
                }
            }
            return list;
        }

        public bool CheckColumnName(MySqlDataReader reader, string columnName)  
        {  
            bool result = false;  
            DataTable dt = reader.GetSchemaTable();
            foreach (DataRow dr in dt.Rows)  
            {  
                if (dr["ColumnName"].ToString() == columnName)  
                {  
                    result = true;  
                }  
            }  
  
            return result;
        }  
        #endregion

        
    }
}
