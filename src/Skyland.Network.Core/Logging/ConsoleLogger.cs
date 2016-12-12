#region using

using System;
using System.Threading;

#endregion

namespace Skyland.Network.Core.Logging
{
    class ConsoleLogger : ILog
    {
        private readonly Type _type;
        private readonly ConsoleLoggerColors _colors;

        public ConsoleLogger(Type type)
        {
            _type = type;
            _colors = new ConsoleLoggerColors();
        }

        public void Trace(string format, params object[] parameters)
        {
            Write(LogLevel.Trace, format, parameters);
        }

        public void Debug(string format, params object[] parameters)
        {
            Write(LogLevel.Debug, format, parameters);
        }

        public void Info(string format, params object[] parameters)
        {
            Write(LogLevel.Info, format, parameters);
        }

        public void Warn(string format, params object[] parameters)
        {
            Write(LogLevel.Warn, format, parameters);
        }

        public void Error(string format, params object[] parameters)
        {
            Write(LogLevel.Error, format, parameters);
        }

        public void Error(Exception exception)
        {
            Write(LogLevel.Error, exception.ToString());
        }

        private void Write(LogLevel level, string format, params object[] parameters)
        {
            var backgroundColor = _colors.GetBackgroundColor(level);
            if (backgroundColor.HasValue)
                Console.BackgroundColor = backgroundColor.Value;

            var foregroundColor = _colors.GetForegroundColor(level);
            if (foregroundColor.HasValue)
                Console.ForegroundColor = foregroundColor.Value;

            var arg1 = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss.ffff");
            var arg2 = Thread.CurrentThread.Name ?? string.Format("Thread Id:{0}", Thread.CurrentThread.ManagedThreadId);
            var arg4 = level.ToString().ToUpperInvariant();

            Console.WriteLine("{0}|{1}|{2}|({3}): {4}", arg1, arg2, _type.FullName, arg4, string.Format(format, parameters));

            Console.ResetColor();
        }
    }
}
