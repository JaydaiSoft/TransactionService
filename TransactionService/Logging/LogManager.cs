using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionServices.Logging
{
    public class LogManager : ILogManager
    {
        private static Logger instance = NLog.LogManager.GetCurrentClassLogger();
        public Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = NLog.LogManager.GetCurrentClassLogger();

                return instance;
            }
        }
    }
}
