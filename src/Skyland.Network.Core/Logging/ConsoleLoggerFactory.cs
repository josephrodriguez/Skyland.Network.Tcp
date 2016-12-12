#region using

using System;
using System.Collections.Concurrent;

#endregion

namespace Skyland.Network.Core.Logging
{
    public class ConsoleLoggerFactory : LoggerFactory
    {
        private readonly ConcurrentDictionary<Type, ILog> _loggers;

        public ConsoleLoggerFactory()
        {
            _loggers = new ConcurrentDictionary<Type, ILog>();   
        }

        protected override ILog GetLogger(Type type)
        {
            ILog log;
            if (_loggers.TryGetValue(type, out log))
                return log;

            log = new ConsoleLogger(type);
            _loggers.TryAdd(type, log);
            return log;
        }
    }
    
}
