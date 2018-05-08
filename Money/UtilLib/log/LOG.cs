using GameLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDb.Util
{
    public class Log
    {
        static public LogImplement __log = new LogImplement(log4net.LogManager.GetLogger("lumao"));
        static public void log(object str) {
            __log.debug(str);
        }
        static public void error(object str)
        {
            __log.error(str);
        }
        static public void warn(object str)
        {
            __log.warning(str);
        }
        static public void info(object str)
        {
            __log.info(str);
        }
        static public void debug(object str)
        {
            __log.debug(str);
        }
        //
        static public void error(object str,Exception e)
        {
            __log.error(str,e);
        }
        static public void warn(object str, Exception e)
        {
            __log.warning(str, e);
        }
        static public void info(object str, Exception e)
        {
            __log.info(str, e);
        }
        static public void debug(object str, Exception e)
        {
            __log.debug(str, e);
        }
    }
}
