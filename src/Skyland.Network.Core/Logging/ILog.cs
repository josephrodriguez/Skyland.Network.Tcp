#region using

using System;

#endregion

namespace Skyland.Network.Core.Logging
{
    public interface ILog
    {
        void Trace(string format, params object[] parameters);

        void Debug(string format, params object[] parameters);

        void Info(string format, params object[] parameters);

        void Warn(string format, params object[] parameters);

        void Error(string format, params object[] parameters);
        void Error(Exception exception);
    }
}
