#region using

using System;
using System.Net;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;
using Skyland.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace Skyland.Network.Tcp.Server.Configuration.Builders
{
    public class NetworkConfigurationBuilder : Builder<INetworkConfiguration>
    {
        private const int DefaultReadTimeout        = -0x1;
        private const int DefaultWriteTimeout       = -0x1;
        private const int DefaultReceiveTimeout     = 0x0;
        private const int DefaultSendTimeout        = 0x0;
        private const int DefaultReceiveBufferSize  = 0x10000;
        private const int DefaultSendBufferSize     = 0x10000;
        private const int DefaultMessageSize        = 0x10000;
        private const int DefaultAllowedConnections = 0x1;

        private int _allowedConnections;
        private int _readTimeout, _writeTimeout, _maximumMessageSize;
        private int _receiveTimeout;
        private int _sendTimeout;
        private int _receiveBufferSize;
        private int _sendBufferSize;

        public NetworkConfigurationBuilder AllowedConnections(int count)
        {
            _allowedConnections = count;
            return this;
        }

        public NetworkConfigurationBuilder ReadTimeout(int timeout)
        {
            _readTimeout = timeout;
            return this;
        }

        public NetworkConfigurationBuilder WriteTimeout(int timeout)
        {
            _writeTimeout = timeout;
            return this;
        }

        public NetworkConfigurationBuilder MaximumMessageSize(int size)
        {
            if(size <= 0)
                throw new ArgumentOutOfRangeException();

            _maximumMessageSize = size;
            return this;
        }

        public NetworkConfigurationBuilder ReceiveTimeout(int timeout)
        {
            _receiveTimeout = timeout;
            return this;
        }

        public NetworkConfigurationBuilder SendTimeout(int timeout)
        {
            _sendTimeout = timeout;
            return this;
        }

        public NetworkConfigurationBuilder ReceiveBufferSize(int timeout)
        {
            _receiveBufferSize = timeout;
            return this;
        }

        public NetworkConfigurationBuilder SendBufferSize(int timeout)
        {
            _sendBufferSize = timeout;
            return this;
        }

        public NetworkConfigurationBuilder OnMessageReceived(Action<Message> action)
        {
            if (action == null) throw new ArgumentNullException();

            //_events.OnMessageReceived += new MessageReceivedEventHandler(action);
            return this;
        }

        public NetworkConfigurationBuilder OnClientConnected(Action<EndPoint> action)
        {
            if (action == null) throw new ArgumentNullException();

            //_events.OnClientConnected += new ClientConnectedEventHandler(action);
            return this;
        }

        public NetworkConfigurationBuilder OnClientDisconnected(Action<EndPoint> action)
        {
            if (action == null) throw new ArgumentNullException();

            //_events.OnClientDisconnected += new ClientDisconnectedEventHandler(action);
            return this;
        }

        public NetworkConfigurationBuilder OnClientAccepted(Action<EndPoint> action)
        {
            if (action == null) throw new ArgumentNullException();

            //_events.OnClientAccepted += new ClientAcceptedEventHandler(action);
            return this;
        }

        public NetworkConfigurationBuilder OnClientRejected(Action<EndPoint> action)
        {
            if (action == null) throw new ArgumentNullException();

            //_events.OnClientRejected += new ClientRejectedEventHandler(action);
            return this;
        }

        internal override INetworkConfiguration Build()
        {
            SetDefaults();

            return 
                new NetworkConfiguration
                {
                    WriteTimeout = _writeTimeout,
                    ReadTimeout =  _readTimeout,
                    ReceiveTimeout = _receiveTimeout,
                    SendTimeout = _sendTimeout,
                    SendBufferSize = _sendBufferSize,
                    ReceiveBufferSize = _receiveBufferSize,
                    AllowedConnections = _allowedConnections,
                    MaximunMessageSize = _maximumMessageSize
                };
        }

        private void SetDefaults()
        {
            if(_allowedConnections == default (int))
                _allowedConnections = DefaultAllowedConnections;

            if (_readTimeout == default(int))
                _readTimeout = DefaultReadTimeout;

            if (_writeTimeout == default(int))
                _writeTimeout = DefaultWriteTimeout;

            if (_receiveTimeout == default(int))
                _receiveTimeout = DefaultReceiveTimeout;

            if (_sendTimeout == default(int))
                _sendTimeout = DefaultSendTimeout;

            if (_receiveBufferSize == default(int))
                _receiveBufferSize = DefaultReceiveBufferSize;

            if (_sendBufferSize == default(int))
                _sendBufferSize = DefaultSendBufferSize;

            if (_maximumMessageSize == default(int))
                _maximumMessageSize = DefaultMessageSize;
        }
    }
}
