#region using

using System;

#endregion

namespace Skyland.Network.Core.Logging
{
    class NullLogger : ILog
    {
        public void Trace(string format, params object[] parameters) {
        }

        public void Debug(string format, params object[] parameters) {
        }

        public void Info(string format, params object[] parameters) {
        }

        public void Warn(string format, params object[] parameters) {
        }

        public void Error(string format, params object[] parameters) {
        }

        public void Error(Exception exception) {
        }
    }
}
