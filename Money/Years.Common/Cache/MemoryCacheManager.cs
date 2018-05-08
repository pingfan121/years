using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Years.Common.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
        public void Clear()
        {
            foreach (var item in MemoryCache.Default)
            {
                this.Remove(item.Key);
            }
        }

        public bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            return (TEntity)MemoryCache.Default.Get(key);
        }

        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            MemoryCache.Default.Add(key, value, new CacheItemPolicy { SlidingExpiration = cacheTime });
        }
    }
}
