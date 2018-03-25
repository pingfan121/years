using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Database
{
    public class DbFactory
    {
        static public IDbConnect create(DbConfig config)
        {
            IDbConnect cnt = null;
            if (config.DbType == "mysql")
                cnt = new DbMySql();
            else if (config.DbType == "redis")
                cnt = new DbRedis();
            int ir = cnt.init(config);
            if (ir < 0)
                return null;

            return cnt;
        }

        static public void destory(IDbConnect connect)
        {
            if (connect == null)
                return;

            connect.release();
        }
    }
}
