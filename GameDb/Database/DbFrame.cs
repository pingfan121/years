using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using GameLib.Database;
using GameDb.Logic;

namespace hudie.sql
{
    public class IDb
    {

        public DBFrame frame;
        public virtual void processReq(IDbConnect connect)
        {

        }
        public virtual void processRes()
        {

        }
    }

    public class DBFrame
    {
        public IDbConnect connect = null;

        private ConcurrentQueue<IDb> reqs = new ConcurrentQueue<IDb>();
        private ConcurrentQueue<IDb> ress = new ConcurrentQueue<IDb>();

        //参数 数据库名字
        public void Init(string baseaddr,string baseuser, string basepass,string baseport,string basename)
        {
            DbConfig config = new DbConfig();
            config.Host = baseaddr;
            config.Port = baseport;
            config.Name = basename;
            config.User = baseuser;
            config.Pwd = basepass;
            config.CharSet = "utf8";

            connect = new DbMySql();

            connect.init(config);


            //下面可以去读取一些数据数据


            //以上

            //启动线程
            Thread thread = new Thread(SqlDispose);
            thread.IsBackground = true;
            thread.Start();
        }

        public void updatelogic()
        {
            int count = 0;

            while(true)
            {
                IDb re = null;
                if(ress.TryDequeue(out re) == true)
                {
                    //处理
                    re.processRes();

                    count++;

                    if(count >= 10)
                    {
                        break;
                    }

                }
                else
                {
                    break;
                }

            }
        }

        //添加一条
        public void AddReq(IDb req)
        {
            reqs.Enqueue(req);
        }

        //添加一条
        public void AddRes(IDb res)
        {
            ress.Enqueue(res);
        }

        private void SqlDispose()
        {
            while(true)
            {
                IDb req = null;

                if(reqs.TryDequeue(out req))
                {
                    req.frame = this;
                    req.processReq(connect);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}
