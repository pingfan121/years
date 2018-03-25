using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public delegate void EventDispose(string eventname,EventData data);

public class AppEvent
{

    private  static Dictionary<string, HashSet<EventDispose>> dic_events = new Dictionary<string, HashSet<EventDispose>>();

    //添加事件
    public static void AddEvent(string eventname, EventDispose fun)
    {
        if(!dic_events.ContainsKey(eventname))
        {
            dic_events.Add(eventname, new HashSet<EventDispose>());
        }

        dic_events[eventname].Add(fun);
    }

    //删除事件1
    public static void RemoveEvent(string eventname)
    {
        if(dic_events.ContainsKey(eventname))
        {
            dic_events.Remove(eventname);
        }
    }
    //删除事件2
    public static void RemoveEvent(string eventname, EventDispose fun)
    {
        if(dic_events.ContainsKey(eventname))
        {
            dic_events[eventname].Remove(fun);
        }
    }

    //派发事件
    public static void DispatchEvent(string eventname, EventData data)
    {
        if(dic_events.ContainsKey(eventname))
        {
            foreach(EventDispose fun in dic_events[eventname])
            {
                fun(eventname,data);
            }
        }
    }
}

