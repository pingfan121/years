using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Database
{
    public class DbConfig
    {
        // 摘要：
        //      数据库类型, 目前仅支持mysql, redis
        public string DbType { get; set; }
        // 摘要: 
        //      数据库主机地址
        public string Host { get; set; }
        // 摘要: 
        //      数据库主机端口
        public string Port { get; set; }
        // 摘要: 
        //      使用的数据库名
        public string Name { get; set; }
        // 摘要: 
        //      数据库访问用户名
        public string User { get; set; }
        // 摘要: 
        //      数据库访问密码
        public string Pwd { get; set; }
        // 摘要: 
        //      数据库连接池最小值, 该连接池由MySql Connector/NET维护
        public string MinPoolSize { get; set; }
        // 摘要: 
        //      数据库连接池最大值, 该连接池由MySql Connector/NET维护
        public string MaxPoolSize { get; set; }
        // 摘要: 
        //      数据库连接字符集
        public string CharSet { get; set; }
    }
}
