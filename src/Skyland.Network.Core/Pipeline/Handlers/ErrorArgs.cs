#region using

using System;

#endregion

namespace Skyland.Network.Core.Pipeline.Handlers
{
    public class ErrorArgs<TElement>
    {
        public TElement Element { get; private set; }
        public Exception Exception { get; private set; }

        public ErrorArgs(TElement element, Exception exception)
        {
            Element = element;
            Exception = exception;
        }
    }
}
