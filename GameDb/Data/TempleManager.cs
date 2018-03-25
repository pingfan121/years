using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Collections;
using System.Reflection;
using GameServer.Define.EnumNormal;
using GameLib.Util;
using System.Globalization;
using System.Threading;
namespace GameLib.game
{
    public class TempleManager
    {
        static private LogImplement log= LogFactory.getLogger(typeof(TempleManager));

        private volatile static TempleManager _instance;

        
        public Dictionary<int,Hashtable> attrs;//以id作为索引，属性作为值,//总表

        //
        Dictionary<String, Type> enumType = new Dictionary<string, Type>();

        public Type getType(string en)
        {
            if (!enumType.ContainsKey(en))
            {
                Type t = Type.GetType("GameServer.Define.EnumNormal." + en);
                if (t != null)
                {
                    enumType[en] = t;
                }
                else
                {
                    throw new Exception("不存在枚举=" + en);
                }
            }
            return enumType[en];
        }
        List<string> paths = new List<string>();

     //   public static Dictionary<string, Dictionary<EnumAttrType, int>> itemAttrs;
        public static Dictionary<Type, Action> callbacks = new Dictionary<Type, Action>();
        //
        public static void regCallBack<T>(Action func)
        {
            Type t = typeof(T);
            if (callbacks.ContainsKey(t)) {
                throw new Exception("已经有这个类的回调函数=" + t);
            }
            callbacks[t]=func;
        }
        public TempleManager()
        {
            CultureInfo tInfo = Thread.CurrentThread.CurrentCulture;
            ArrayList files=JSON.Decode(File.ReadAllText("config/config.json")) as ArrayList;
            //
            for (int n = 0; n < files.Count; n++)
            {
                string filename = (files[n] as Hashtable)["tablename"].ToString();
                string[] arr = filename.Split('_');
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = tInfo.TextInfo.ToTitleCase(arr[i]);
                }
                var classname = "Tb" + string.Join<string>("", arr);
                paths.Add(classname);
                //LOG.log(filename);
                initTable(paths[n]);
            }
            string path = Environment.CurrentDirectory + @"\data\";
            watcherChange(path, "*.*");
        }
        FileSystemWatcher _watcher;
        public HashSet<string> changefile = new HashSet<string>();
        private Timer m_timer;

        private void watcherChange(string path, string filter)
        {
            _watcher = new FileSystemWatcher();
            _watcher.Path = path;
            _watcher.Filter = filter;
            _watcher.Changed += new FileSystemEventHandler(onWatchProcess);
            _watcher.Created += new FileSystemEventHandler(onWatchProcess);
            _watcher.Deleted += new FileSystemEventHandler(onWatchProcess);
            _watcher.Renamed += new RenamedEventHandler(onWatchProcess);
            _watcher.EnableRaisingEvents = true;
            // watcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName| NotifyFilters.Size;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            // Create the timer that will be used to deliver events. Set as disabled
            m_timer = new Timer(new TimerCallback(OnWatchedFileChange), null, Timeout.Infinite, Timeout.Infinite);
            _watcher.IncludeSubdirectories = true;
        }
        DateTime updateTime = DateTime.Now.AddDays(100);//
        private void OnWatchedFileChange(object state)
        {
            updateTime = DateTime.Now;
        }

        private void onWatchProcess(object source, FileSystemEventArgs e)
        {
            //string ext = ".txt";
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    {
                        //                         ent = new ScriptFileChangeEvent();
                        //                         ent.Load = searchFile(e.FullPath, ext);
                        //                         ent.Unload = null;
                        break;
                    }
                case WatcherChangeTypes.Deleted:
                    {
                        //                         ent = new ScriptFileChangeEvent();
                        //                         ent.Load = null;
                        //                         ent.Unload = e.FullPath;
                        break;
                    }
                case WatcherChangeTypes.Changed:
                    {
                        Monitor.Enter(changefile);
                        try
                        {
                            changefile.Add(e.Name.Substring(0, e.Name.Length - 4));
                            m_timer.Change(500, Timeout.Infinite);//500毫秒后触发
                        }
                        finally {
                            Monitor.Exit(changefile);
                        }
                        //log.warning(string.Format("文件发生变化:{0}", e.Name));   
                        break;
                    }
                case WatcherChangeTypes.Renamed:
                    {
                        break;
                    }
            }
        }
        public void updateFile()
        {
            if (DateTime.Now > updateTime) {
                if (Monitor.TryEnter(changefile))
                {
                    updateTime = DateTime.Now.AddDays(100);
                    try
                    {
                        if (changefile.Count > 0)
                        {
                            //Thread.CurrentThread.Sleep(1);
                            foreach (string file in changefile)
                            {
                                try
                                {
                                    log.warning(string.Format("更新数据表:{0}", file));
                                    initTable(file);
                                }
                                catch (Exception e)
                                {
                                    log.error(file + ">数据表更新发生错误", e);
                                }
                            }

                            changefile.Clear();
                        }
                    }
                    finally
                    {
                        Monitor.Exit(changefile);
                    }
                }
            }
            
        }

        public bool initTable(string file)
        {
            if (paths.IndexOf(file) < 0)
            {
                log.error(file + ">不存在");
                return false;
            }
            //

            Type retype = Type.GetType("GameDb.Data." + file);


            string tablename = file;
            string path = file + ".txt";
            attrs = new Dictionary<int, Hashtable>();
            //
            string[] readText = File.ReadAllLines("data/" + path, Encoding.UTF8);//GetEncoding("gb2312"));    
            //以第一行作为表头，建立索引
            Dictionary<string, int> indexobj = new Dictionary<string, int>();//表头的索引数组
            //
            string[] indexarr = (readText[1].Trim()).Split('\t');//英文作为索引
            CultureInfo tInfo = Thread.CurrentThread.CurrentCulture;
            for (int i = 0; i < indexarr.Length; i++)
            {
                if (indexarr[i] == "")
                {
                    break;
                }
                //
                string[] arr = indexarr[i].Split('_');
                if (arr.Length > 1)
                {
                    for (int z = 0; z < arr.Length; z++)
                    {
                        arr[z] = tInfo.TextInfo.ToTitleCase(arr[z]);
                    }
                    indexarr[i] = string.Join<string>("", arr);
                }
                else
                {
                    indexarr[i] = indexarr[i].Substring(0, 1).ToUpper() + indexarr[i].Substring(1);

                }
                //tInfo.TextInfo.ToTitleCase(indexarr[i].Trim());
            }
            for (int i = 0; i < indexarr.Length; i++)
            {
                indexobj.Add(indexarr[i].Trim(), i);
            }
            //第三行是字段类型
            string[] typearr = (readText[2].Trim()).Split('\t');
            for (int i = 0; i < typearr.Length; i++)
            {
                typearr[i] = typearr[i].Trim();
            }
            //后面的是数据
            for (int i = 3; i < readText.Length; i++)
            {
                string[] strs = readText[i].Split('\t');
                if (strs[0].Trim().Length < 1)
                {
                    continue;
                }
                if (strs[0].Trim().IndexOf('#') == 0)
                {//#开头的行忽略
                    continue;
                }
                Hashtable Record = new Hashtable();
                foreach (KeyValuePair<string, int> entry in indexobj)
                {
                    // tb.Add(entry.Key, strs[entry.Value]);
                    string type = typearr[entry.Value];
                    if (type.IndexOf('[') > 0)
                    {   //数组
                        type = type.Substring(0, type.IndexOf('['));
                        if (type == "double")
                        {
                            string[] sl = strs[entry.Value].Split(',');
                            double[] dl = new double[sl.Length];
                            for (int t = 0; t < dl.Length; t++)
                            {
                                dl[t] = double.Parse(sl[t]);
                            }
                            Record.Add(entry.Key, dl);
                        }
                        else if (type == "float")
                        {
                            if (strs[entry.Value].Length > 0)
                            {
                                string[] sl = strs[entry.Value].Split(',');
                                float[] fl = new float[sl.Length];
                                for (int t = 0; t < fl.Length; t++)
                                {
                                    fl[t] = float.Parse(sl[t]);
                                }
                                Record.Add(entry.Key, fl);
                            }
                            else
                            {
                                Record.Add(entry.Key, new float[0]);
                            }
                        }
                        else if (type == "int")
                        {
                            if (strs[entry.Value].Length > 0)
                            {
                                string[] sl = strs[entry.Value].Split(',');
                                int[] il = new int[sl.Length];
                                for (int t = 0; t < il.Length; t++)
                                {
                                    il[t] = int.Parse(sl[t]);
                                }
                                Record.Add(entry.Key, il);
                            }
                            else
                            {
                                Record.Add(entry.Key, new int[0]);
                            }

                        }
                        else if (type == "short")
                        {
                            if (strs[entry.Value].Length > 0)
                            {
                                string[] sl = strs[entry.Value].Split(',');
                                short[] il = new short[sl.Length];
                                for (int t = 0; t < il.Length; t++)
                                {
                                    il[t] = short.Parse(sl[t]);
                                }
                                Record.Add(entry.Key, il);
                            }
                            else
                            {
                                Record.Add(entry.Key, new int[0]);
                            }

                        }
                        else if (type == "bool")
                        {
                            string[] sl = strs[entry.Value].Split(',');
                            bool[] bl = new bool[sl.Length];
                            for (int t = 0; t < bl.Length; t++)
                            {
                                if (sl[t] == "0" || sl[t] == "false")
                                {
                                    bl[t] = false;
                                }
                                else
                                {
                                    bl[t] = true;
                                }
                            }
                            Record.Add(entry.Key, bl);
                        }
                        else if (type == "string")
                        {
                            if (strs[entry.Value].Length > 0)
                            {
                                Record.Add(entry.Key, strs[entry.Value].Split(','));
                            }
                            else
                            {
                                Record.Add(entry.Key, new string[0] { });
                            }
                        }
                        else
                        {
                            //throw new Exception("不支持。。。。枚举数组");
                            //枚举类型怎么处理呢。。。
                            Type ty = getType(type) as Type;
                            string[] sl = strs[entry.Value].Split(',');
                            int[] el = new int[sl.Length];

                            for (int t = 0; t < el.Length; t++)
                            {
                                el[t] = (int)Enum.Parse(ty, sl[t]);
                            }
                            Record.Add(entry.Key, el);
                        }
                    }
                    else
                    {
                        if (type == "float")
                        {

                            Record.Add(entry.Key, float.Parse(strs[entry.Value]));

                        }
                        else if (type == "double")
                        {
                            Record.Add(entry.Key, double.Parse(strs[entry.Value]));
                        }
                        else if (type == "int")
                        {
                            int rer = 0;
                            if (int.TryParse(strs[entry.Value], out rer))
                            {
                                Record.Add(entry.Key, rer);
                            }
                            else
                            {
                                Record.Add(entry.Key, 0);
                            }

                        }
                        else if (type == "long")
                        {
                            //======
                            long rer = 0;
                            if (long.TryParse(strs[entry.Value], out rer))
                            {
                                Record.Add(entry.Key, rer);
                            }
                            else
                            {
                                Record.Add(entry.Key, 0);
                            }
                        }
                        else if (type == "short")
                        {
                            float re = 0;
                            if (float.TryParse(strs[entry.Value], out re))
                            {
                                Record.Add(entry.Key, (short)re);
                            }
                            else
                            {
                                Record.Add(entry.Key, 0);
                            }

                        }
                        else if (type == "bool")
                        {
                            if (strs[entry.Value] == "0" || strs[entry.Value] == "false")
                            {
                                Record.Add(entry.Key, false);
                            }
                            else
                            {
                                Record.Add(entry.Key, true);
                            }
                        }
                        else if (type == "string")
                        {
                            Record.Add(entry.Key, strs[entry.Value]);
                        }
                        else
                        { //枚举类型
                            Type ty = getType(type);
                            Record.Add(entry.Key, Enum.Parse(ty, strs[entry.Value]));
                        }
                    }

                }
                if (Record.Contains("Id"))
                {
                    attrs.Add((int)Record["Id"], Record);//第一列为ID
                }
                else
                {
                    attrs.Add(int.Parse(strs[0]), Record);//第一列为ID
                }
            }
            MethodInfo method = retype.GetMethod("initdata");
            method.Invoke(null, new object[] { attrs }); //第一个参数忽略

           
            if (callbacks.ContainsKey(retype))
            {
                callbacks[retype]();//回调
            }
           
            return true;
        }
        public static TempleManager Instance()
        {
            if (_instance == null)
            {
                _instance = new TempleManager();
            }
            return _instance;
        }
    }
}
