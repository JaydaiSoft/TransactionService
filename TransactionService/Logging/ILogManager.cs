using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionService.Logging
{
    public interface ILogManager
    {
        Logger Instance { get; }
    }
}
