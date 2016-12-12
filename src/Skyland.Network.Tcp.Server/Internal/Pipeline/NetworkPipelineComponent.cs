#region using

using System;
using System.Net.Sockets;
using Skyland.Network.Core.Pipeline;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace Skyland.Network.Tcp.Server.Internal.Pipeline
{
    class NetworkPipelineComponent : IPipelineComponent<TcpClient>
    {
        private readonly INetworkConfiguration _configuration;

        public NetworkPipelineComponent(INetworkConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Execute(PipelineElement<TcpClient> arg)
        {
            //Configure buffers size
            arg.Argument.SendBufferSize = _configuration.SendBufferSize;
            arg.Argument.ReceiveBufferSize = _configuration.ReceiveBufferSize;

            //Configure timeouts
            arg.Argument.SendTimeout = _configuration.SendTimeout;
            arg.Argument.ReceiveTimeout = _configuration.ReceiveTimeout;

            var stream = arg.Argument.GetStream();
            if (stream == null)
                throw new Exception();

            stream.ReadTimeout = _configuration.ReadTimeout;
            stream.WriteTimeout = _configuration.WriteTimeout;
        }
    }
}
