#region using

using System.Net.Sockets;
using Skyland.Network.Core.Pipeline;

#endregion

namespace Skyland.Network.Tcp.Server.Internal.Pipeline
{
    class FilterComponent : IPipelineComponent<TcpClient>
    {
        public void Execute(PipelineElement<TcpClient> arg)
        {
        }
    }
}
