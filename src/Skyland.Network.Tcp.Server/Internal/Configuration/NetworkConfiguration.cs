using System.Net;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;
using Skyland.Network.Tcp.Server.Handlers;

namespace Skyland.Network.Tcp.Server.Internal.Configuration
{
    internal class NetworkConfiguration : INetworkConfiguration
    {
        internal event MessageReceivedEventHandler OnMessageReceived;

        internal event ClientAcceptedEventHandler OnClientAccepted;

        internal event ClientRejectedEventHandler OnClientRejected;

        internal event ClientConnectedEventHandler OnClientConnected;

        internal event ClientDisconnectedEventHandler OnClientDisconnected;

        public int AllowedConnections { get; internal set; }
        public int MaximunMessageSize { get; internal set; }
        public int ReadTimeout { get; internal set; }
        public int WriteTimeout { get; internal set; }
        public int ReceiveBufferSize { get; internal set; }
        public int SendBufferSize { get; internal set; }
        public int SendTimeout { get; internal set; }
        public int ReceiveTimeout { get; internal set; }

        public void RaiseClientConnected(EndPoint endpoint)
        {
            if (OnClientConnected == null) return;
            OnClientConnected(endpoint);
        }

        public void RaiseClientDisconnected(EndPoint endpoint)
        {
            if (OnClientDisconnected == null) return;
            OnClientDisconnected(endpoint);
        }

        public void RaiseClientAccepted(EndPoint endpoint)
        {
            if (OnClientAccepted == null) return;
            OnClientAccepted(endpoint);
        }

        public void RaiseClientRejected(EndPoint endpoint)
        {
            if (OnClientRejected == null) return;
            OnClientRejected(endpoint);
        }

        public void RaiseMessageReceived(Message message)
        {
            if (OnMessageReceived == null) return;

            OnMessageReceived(message);
        }
    }
}
