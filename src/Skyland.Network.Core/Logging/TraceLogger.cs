using System;

namespace Skyland.Network.Core.Logging
{
    class TraceLogger : ILog
    {
        private readonly Type _type;

        public TraceLogger(Type type) {
            _type = type;
        }

        public void Trace(string format, params object[] parameters)
        {
            System.Diagnostics.Trace.TraceInformation(_type + ": " + format, parameters);
        }

        public void Debug(string format, params object[] parameters)
        {
            System.Diagnostics.Trace.TraceInformation(_type + ": " + format, parameters);
        }

        public void Info(string format, params object[] parameters)
        {
            System.Diagnostics.Trace.TraceInformation(_type + ": " + format, parameters);
        }

        public void Warn(string format, params object[] parameters)
        {
            System.Diagnostics.Trace.TraceWarning(_type + ": " + format, parameters);
        }

        public void Error(string format, params object[] parameters)
        {
            System.Diagnostics.Trace.TraceError(_type + ": " + format, parameters);
        }

        public void Error(Exception exception)
        {
            System.Diagnostics.Trace.TraceError(_type + ": " + exception);
        }
    }
}
