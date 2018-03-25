using System;
using System.Collections.Generic;
using System.Text;

namespace util
{

    public class HashSet<T>
    {
        public Dictionary<T, bool> keys;
        public HashSet()
        {
            this.keys = new Dictionary<T,bool>();
        }

        internal void Add(T key)
        {
            if (keys.ContainsKey(key))
            {
            }
            else
            {
                keys.Add(key,true);
            }
        }
        internal void Add(T[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                Add(keys[i]);
            }
        }
        internal bool Contains(T key)
        {
            return keys.ContainsKey(key);
        }
    }
}
