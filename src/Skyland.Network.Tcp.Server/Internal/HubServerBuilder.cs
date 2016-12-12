#region using

using System;
using System.Net;
using Skyland.Network.Core.Asserts;
using Skyland.Network.Core.Logging;
using Skyland.Network.Tcp.Server.Configuration.Builders;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace Skyland.Network.Tcp.Server.Internal
{
    public class ServerBuilder
    {
        private ISslConfiguration _sslConfiguration;
        private ICompressConfiguration _compressionConfig;
        private INetworkConfiguration _networkConfiguration;

        private readonly IPEndPoint _endpoint;

        protected internal ServerBuilder(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ServerBuilder Compression(Action<CompressionBuilder> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_compressionConfig);

            var builder = new CompressionBuilder();
            action(builder);

            _compressionConfig = builder.Build();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ServerBuilder Ssl(Action<SslConfigurationBuilder> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_sslConfiguration);

            var configurer = new SslConfigurationBuilder();
            action(configurer);

            //Build SSL configuration
            _sslConfiguration = configurer.Build();

            return this;
        }

        public ServerBuilder Network(Action<NetworkConfigurationBuilder> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_networkConfiguration);

            var configurer = new NetworkConfigurationBuilder();
            action(configurer);

            //Build optional configuration
            _networkConfiguration = configurer.Build();

            return this;
        }

        public ServerBuilder Logging(Action<LoggingBuilder> action)
        {
            var builder = new LoggingBuilder();
            action(builder);

            LoggerFactory.Current = builder.Build();
            return this;
        }

        /// <summary>
        /// Create an instance of <see cref="P:RoyalSoft.Network.Tcp.Server.IHubServer"/> with configuration.
        /// </summary>
        /// <returns>Create an instance of <see cref="P:RoyalSoft.Network.Tcp.Server.IHubServer"/> with configuration.</returns>
        public IHubServer Create()
        {
            if (_sslConfiguration == null)
                _sslConfiguration = new SslConfigurationBuilder().Build();

            if(_compressionConfig == null)
                _compressionConfig = new CompressionBuilder().Build();

            if (_networkConfiguration == null)
                _networkConfiguration = new NetworkConfigurationBuilder().Build();

            return
                new HubServer(_endpoint, _sslConfiguration, _compressionConfig, _networkConfiguration);
        }
    }
}
