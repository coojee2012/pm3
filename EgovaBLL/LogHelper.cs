using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace EgovaBLL
{
    public class LogHelper
    {
        private ILog logger;
        public LogHelper(Type type)
        {
            logger = LogManager.GetLogger(type);
        }
       public void Debug(object message)
        {
            logger.Debug(message);
        }
       public void Error(object message)
        {
            logger.Error(message);
        }
       public void Fatal(object message)
        {
            logger.Fatal(message);
        }
       public void Info(object message)
        {
            logger.Info(message);
        }
       public void Warn(object message)
        {
            logger.Warn(message);
        }

    }
}
