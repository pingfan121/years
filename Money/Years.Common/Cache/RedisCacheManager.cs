using GameLib.Util;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Common.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly string redisConnenctionString;

        public volatile ConnectionMultiplexer redisConnection;

        private readonly object redisConnectionLock = new object();

        public RedisCacheManager()
        {
            //链接redis服务语句
            string redisConfiguration = ConfigurationManager.ConnectionStrings["redisCache"].ToString();

            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            redisConnenctionString = redisConfiguration;
            redisConnection = GetRedisConnection();
        }

        private ConnectionMultiplexer GetRedisConnection()
        {
            if (redisConnection != null && redisConnection.IsConnected)
            {
                return redisConnection;
            }

            lock (redisConnectionLock)
            {
                if (redisConnection != null)
                {
                    //释放redis连接
                    redisConnection.Dispose();
                }
                redisConnection = ConnectionMultiplexer.Connect(redisConnenctionString);
            }
            return redisConnection;
        }

        public void Clear()
        {
            foreach (var endPoint in GetRedisConnection().GetEndPoints())
            {
                var server = GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        public bool Contains(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                return JSON.Decode<TEntity>(value.ToString());
            }
            else
            {
                return default(TEntity);
            }
        }

        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                redisConnection.GetDatabase().StringSet(key, JSON.Encode(value), cacheTime);
            }
        }
    }
}
