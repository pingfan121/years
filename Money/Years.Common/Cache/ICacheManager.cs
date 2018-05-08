using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Common.Cache
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);
        //设置
        void Set(string key, object value, TimeSpan cacheTime);
        //判断是否存在
        bool Contains(string key);
        //移除
        void Remove(string key);
        //清除
        void Clear();

    }
}
