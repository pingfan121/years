using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameLib.Util
{
    public class LogHelper
    {
        static public StringBuilder _log = new StringBuilder();
        static FileIO fio;
        static public int min = 0;
        static public int max = 10;

        static LogHelper()
        {
            fio = new FileIO();
            fio.OpenWriteFile("d:\\log.txt");
        }
        static public void error(string str)
        {
            log(str, 10);

        }
        static public void log(string str, int level)
        {
            if (str == null)
            {
                str = "null";
            }
            if (level <= max && level >= min)
            {
                Console.WriteLine(DateTime.Now.ToLocalTime() + "->" + str);
            }
            fio.WriteLine(DateTime.Now.ToLocalTime() + "->" + str + "\r\n");

        }
        internal static void log(object p, int level)
        {
            log(p.ToString(), level);
        }
        static public void log(object str)
        {
            log(str, 0);
        }
    }
}