using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameLib.Util
{
    public class LogFactory
    {
        static LogFactory()
        {
            String directionry = AppDomain.CurrentDomain.BaseDirectory;

            FileInfo configFile = new FileInfo(directionry + @"Log.config");

            // 用来配置框架并检测文件的变化
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
        }

        public static LogImplement getLogger(Type type)
        {
            return new LogImplement(log4net.LogManager.GetLogger(type));
        }

        public static LogImplement getLogger(string str)
        {
            return new LogImplement(log4net.LogManager.GetLogger(str));
        }
    }
}