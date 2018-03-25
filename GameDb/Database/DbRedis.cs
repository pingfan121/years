using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System.Reflection;
using Easy4net.CustomAttributes;
using System.Collections;
using GameDb.Logic;

namespace GameLib.Database
{
    public class DbRedis : IDbConnect
    {
        class TbField
        {
            public string Name { get; set; }
            public object Value { get; set; }

            public TbField()
            {
                Name = string.Empty;
                Value = null;
            }
        }

        private static PooledRedisClientManager _prcm;

        public DbRedis()
        {
        }

        public int init(DbConfig config)
        {
            string host = "";
            if(config.Pwd == null || config.Pwd == "")
                host = string.Format("{0}:{1}", config.Host, config.Port);
            else
                host = string.Format("{0}@{1}:{2}", config.Pwd, config.Host, config.Port);
            string[] hostArray = new string[] { host };
            _prcm = new PooledRedisClientManager(hostArray, hostArray, 
                        new RedisClientManagerConfig
                            {
                                MaxWritePoolSize = 60,
                                MaxReadPoolSize = 60,
                                AutoStart = true,
                            });
            return 0;
        }

        public void release()
        {
        }

        public List<T> find<T>(string query) where T : TbLogic,new()
        {
            using (var redisClient = getClient())
            {
                var client = redisClient.As<T>();
                T t = default(T);
                string key = getPrimaryKey<T>(query);
                t = client.GetValue(key);
                return new List<T>() { t };
            }
        }

        public int insert<T>(T t) where T : TbLogic
        {
            string key = getPrimaryKey(t);
            if (key == null || key == string.Empty)
                return -1;

            using (var redisClient = getClient())
            {
                var client = redisClient.As<T>();
                if (client.ContainsKey(key) == true)
                    return -2;

                client.SetEntry(key, t);
            }

            return 0;
        }

        public int update<T>(T t) where T : TbLogic
        {
            string key = getPrimaryKey(t);
            if (key == null || key == string.Empty)
                return -1;

            using (var redisClient = getClient())
            {
                var client = redisClient.As<T>();
                if (client.ContainsKey(key) == false)
                    return -2;

                client.SetEntry(key, t);
            }

            return 0;
        }

        public int delete<T>(T t) where T : TbLogic
        {
            string key = getPrimaryKey(t);
            if (key == null || key == string.Empty)
                return -1;

            using (var redisClient = getClient())
            {
                var client = redisClient.As<T>();
                client.RemoveEntry(key);
            }
            return 0;
        }

        public int insert<T>(List<T> listT) where T : TbLogic
        {
            if (listT == null)
                return -1;

            foreach(var v in listT)
                insert<T>(v);

            return 0;
        }

        public int update<T>(List<T> listT) where T : TbLogic
        {
            if (listT == null)
                return -1;

            foreach (var v in listT)
                update<T>(v);

            return 0;
        }

        public int delete<T>(List<T> listT) where T : TbLogic
        {
            if (listT == null)
                return -1;

            foreach (var v in listT)
                delete<T>(v);

            return 0;
        }

        public int executeNonQuery(string sql, Hashtable param)
        {
            // ...啥都不做
            return 0;
        }

        private IRedisClient getClient()
        {
            if (_prcm == null)
                return null;

            return _prcm.GetClient();
        }

        private string getPrimaryKey<T>(object key)
        {
            if(key == null)
                return string.Format("{0}", typeof(T).Name);
            else
                return string.Format("{0}:{1}", typeof(T).Name, key);
        }

        private string getPrimaryKey<T>(T t)
        {
            TbField tf = getPrimaryField<T>(t);
            if (tf == null)
                return "";

            if(tf.Value == null)
                return string.Format("{0}", t.GetType().Name);
            else
                return string.Format("{0}:{1}", t.GetType().Name, tf.Value);
        }

        private TbField getPrimaryField<T>(T t)
        {
            PropertyInfo[] properties = t.GetType().GetProperties();
            if (properties == null)
                return null;

            foreach (PropertyInfo property in properties)
            {
                object[] propertyAttrs = property.GetCustomAttributes(false);
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    object propertyAttr = propertyAttrs[i];
                    if (propertyAttr is IdAttribute)
                    {
                        TbField tf = new TbField();
                        tf.Name = property.Name;
                        tf.Value = property.GetValue(t, null);
                        return tf;
                    }
                }
            }

            return null;
        }


        public bool Connected
        {
            get { return true; }
        }
    }
}
