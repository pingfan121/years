using System;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using Easy4net.Common;

namespace Easy4net.DBUtility
{
    public class DbFactory
    {
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public static string CreateDbParmCharacter()
        {
            string character = string.Empty;

            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    character = "@";
                    break;
                case DatabaseType.ORACLE:
                    character = ":";
                    break;
                case DatabaseType.MYSQL:
                    character = "?";
                    break;
                case DatabaseType.ACCESS:
                    character = "@";
                    break;
                case DatabaseType.SQLITE:
                    character = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return character;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型和传入的
        /// 数据库链接字符串来创建相应数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// 
        public static IDbConnection CreateDbConnection(string connectionString)
        {
            
            
            IDbConnection conn = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    conn = new SqlConnection(connectionString);
                    break;
//                 case DatabaseType.ORACLE:
//                     conn = new OracleConnection(connectionString);
//                     break;
                case DatabaseType.MYSQL:
                    conn = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.ACCESS:
                    conn = new OleDbConnection(connectionString);
                    break;
                case DatabaseType.SQLITE:
                    conn = new SQLiteConnection(connectionString);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return conn;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库命令对象
        /// </summary>
        /// <returns></returns>
        public static IDbCommand CreateDbCommand()
        {
            IDbCommand cmd = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    cmd = new SqlCommand();
                    break;
//                 case DatabaseType.ORACLE:
//                     cmd = new OracleCommand();
//                     break;
                case DatabaseType.MYSQL:
                    cmd = new MySqlCommand();
                    break;
                case DatabaseType.ACCESS:
                    cmd = new OleDbCommand();
                    break;
                case DatabaseType.SQLITE:
                    cmd = new SQLiteCommand();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return cmd;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库适配器对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter()
        {
            IDbDataAdapter adapter = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    adapter = new SqlDataAdapter();
                    break;
//                 case DatabaseType.ORACLE:
//                     adapter = new OracleDataAdapter();
//                     break;
                case DatabaseType.MYSQL:
                    adapter = new MySqlDataAdapter();
                    break;
                case DatabaseType.ACCESS:
                    adapter = new OleDbDataAdapter();
                    break;
                case DatabaseType.SQLITE:
                    adapter = new SQLiteDataAdapter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return adapter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 和传入的命令对象来创建相应数据库适配器对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter(IDbCommand cmd)
        {
            IDbDataAdapter adapter = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    break;
//                 case DatabaseType.ORACLE:
//                     adapter = new OracleDataAdapter((OracleCommand)cmd);
//                     break;
                case DatabaseType.MYSQL:
                    adapter = new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case DatabaseType.ACCESS:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
                case DatabaseType.SQLITE:
                    adapter = new SQLiteDataAdapter((SQLiteCommand)cmd);
                    break;
                default: throw new Exception("数据库类型目前不支持！");
            }

            return adapter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter()
        {
            IDbDataParameter param = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    param = new SqlParameter();
                    break;
                case DatabaseType.ORACLE:
                    param = new OracleParameter();
                    break;
                case DatabaseType.MYSQL:
                    param = new MySqlParameter();
                    break;
                case DatabaseType.ACCESS:
                    param = new OleDbParameter();
                    break;
                case DatabaseType.SQLITE:
                    param = new SQLiteParameter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, ParameterDirection direction)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = direction;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, int size, ParameterDirection direction)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = direction;
            param.ParameterName = paramName;
            param.Value = value;
            param.Size = size;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbOutParameter(string paramName, int size)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.Size = size;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateDbParameter(string paramName, object value, DbType dbType, ParameterDirection direction)
        {
            if (AdoHelper.DbType == DatabaseType.ACCESS || AdoHelper.DbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = DbFactory.CreateDbParameter();
            param.Direction = direction;
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 和传入的参数来创建相应数据库的参数数组对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter[] CreateDbParameters(int size)
        {
            int i = 0;
            IDbDataParameter[] param = null;
            switch (AdoHelper.DbType)
            {
                case DatabaseType.SQLSERVER:
                    param = new SqlParameter[size];
                    while (i < size) { param[i] = new SqlParameter(); i++; }
                    break;
                case DatabaseType.ORACLE:
                    param = new OracleParameter[size];
                    while (i < size) { param[i] = new OracleParameter(); i++; }
                    break;
                case DatabaseType.MYSQL:
                    param = new MySqlParameter[size];
                    while (i < size) { param[i] = new MySqlParameter(); i++; }
                    break;
                case DatabaseType.ACCESS:
                    param = new OleDbParameter[size];
                    while (i < size) { param[i] = new OleDbParameter(); i++; }
                    break;
                case DatabaseType.SQLITE:
                    param = new SQLiteParameter[size];
                    while (i < size) { param[i] = new SQLiteParameter(); i++; }
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");

            }

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的事物对象
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction CreateDbTransaction(string connectionString)
        {
            IDbConnection conn = CreateDbConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static IDbTransaction CreateDbTransaction(string connectionString, System.Data.IsolationLevel level)
        {
            IDbConnection conn = CreateDbConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction(level);
        } 
    }
}