#region using

using System;

#endregion

namespace Skyland.Network.Core.Logging
{
    public class TraceLoggerFactory : LoggerFactory
    {
        protected override ILog GetLogger(Type type)
        {
            return new TraceLogger(type);
        }
    }
}
