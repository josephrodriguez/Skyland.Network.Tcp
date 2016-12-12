#region using


#endregion

namespace Skyland.Network.Core.Pipeline.Handlers
{
    public delegate void PipelineErrorEventHandler<T>(object sender, ErrorArgs<T> args);
}
