using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Util
{
    public class LogImplement
    {
        private log4net.ILog Logger { get; set; }

        public LogImplement(log4net.ILog log)
        {
            Logger = log;
        }

        public void mdcSet(string key, string val)
        {
            log4net.MDC.Set(key, val);
        }

        public string mdcGet(string key)
        {
            return log4net.MDC.Get(key);
        }

        public void mdcRemove(string key)
        {
            log4net.MDC.Remove(key);
        }

        public void mdcClear()
        {
            log4net.MDC.Clear();
        }
        // ERROR
        public void error(object message)
        {
            if (Logger.IsErrorEnabled)
                Logger.Error(message);
        }

        public void error(object message, Exception e)
        {
            if (Logger.IsErrorEnabled)
                this.Logger.Error(message, e);
        }

        // WARN
        public void warning(object message)
        {
            if (Logger.IsWarnEnabled)
                this.Logger.Warn(message);
        }

        public void warning(object message, Exception e)
        {
            if (Logger.IsWarnEnabled)
                this.Logger.Warn(message, e);
        }
        public void log(object message)
        {
            if (Logger.IsInfoEnabled)
                this.Logger.Info(message);
        }
        // INFO
        public void info(object message)
        {
            if (Logger.IsInfoEnabled)
                this.Logger.Info(message);
        }

        public void info(object message, Exception e)
        {
            if (Logger.IsInfoEnabled)
                this.Logger.Info(message, e);
        }

        // DEBUG
        public void debug(object message)
        {
            if (Logger.IsDebugEnabled)
                this.Logger.Debug(message);
        }

        public void debug(object message, Exception e)
        {
            if (Logger.IsDebugEnabled)
                this.Logger.Debug(message, e);
        }
    }
}
