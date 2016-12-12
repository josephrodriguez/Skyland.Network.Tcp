#region using

using System;

#endregion

namespace Skyland.Network.Core.Logging
{
    public abstract class LoggerFactory : ILoggerFactory
    {
        private static ILoggerFactory _default = new ConsoleLoggerFactory();
        private static ILoggerFactory _current = Default;

        public static ILoggerFactory Current {
            get {
                return _current;
            }
            set {
                if(value == null)
                    throw new ArgumentNullException();
                if(Equals(_current, value))
                    return;
                _current = value;
            }
        }

        public static ILoggerFactory Default
        {
            get
            {
                return _default;
            }
            set
            {
                if(value == null)
                    throw new ArgumentNullException();
                if(Equals(_default, value))
                    return;
                _default = value;
            }
        }

        protected abstract ILog GetLogger(Type type);

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
    }
}
