#region using

using System;
using System.Net.Security;
using System.Net.Sockets;
using Skyland.Network.Core.Logging;
using Skyland.Network.Core.Pipeline;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace Skyland.Network.Tcp.Server.Internal.Pipeline
{
    class SslPipelineComponent : IPipelineComponent<TcpClient>
    {
        private readonly ISslConfiguration _configuration;

        private readonly ILog _log;

        public SslPipelineComponent(ISslConfiguration configuration)
        {
            _configuration = configuration;
            _log = LoggerFactory.Current.GetLogger<SslPipelineComponent>();
        }

        public void Execute(PipelineElement<TcpClient> arg)
        {
            var sslStream = new SslStream(arg.Argument.GetStream(), false);

            try
            {
                sslStream.AuthenticateAsServer(_configuration.Certificate, _configuration.ClientCertificateIsRequired, _configuration.Protocols, true);
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
        }
    }
}
