using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Util
{
    //回收的接口
    public interface IObject{
        void OnRecycle();// //回收到对象池时调用，清理对象
    }
    public class ObjectPool
    {
        static Dictionary<Type, Queue<IObject>> pools = new Dictionary<Type, Queue<IObject>>();
        //回收对象
        static public void recycle(IObject o)
        {
            o.OnRecycle();
            Type t=o.GetType();
            if (!pools.ContainsKey(t)) {
                pools[t] = new Queue<IObject>();

            }
            pools[t].Enqueue(o);
        }
        //获取一个对象
        static public T getObject<T>() where T:class,IObject
        {
            return getObject(typeof(T)) as T;
        }

        static public IObject getObject(Type t)
        {
            if (!pools.ContainsKey(t))
                pools[t] = new Queue<IObject>();

            IObject re = null;
            if (pools[t].Count > 0)
                re = pools[t].Dequeue() as IObject;
            else
                re = Activator.CreateInstance(t) as IObject;
            return re;
        }
    }
}
