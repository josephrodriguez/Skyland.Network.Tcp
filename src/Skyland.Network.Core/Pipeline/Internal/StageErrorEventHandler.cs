#region using

using Skyland.Network.Core.Pipeline.Handlers;

#endregion

namespace Skyland.Network.Core.Pipeline.Internal
{
    public delegate void StageErrorEventHandler<TElement>(object sender, ErrorArgs<TElement> args);
}
