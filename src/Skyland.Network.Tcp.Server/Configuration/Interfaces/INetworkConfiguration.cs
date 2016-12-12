using System.Net;

namespace Skyland.Network.Tcp.Server.Configuration.Interfaces
{
    public interface INetworkConfiguration
    {
        int AllowedConnections { get; }
        int MaximunMessageSize { get; }
        int ReadTimeout { get; }
        int WriteTimeout { get; }

        int ReceiveBufferSize { get; }
        int SendBufferSize { get; }

        int SendTimeout { get; }
        int ReceiveTimeout { get; }

        //Events
        void RaiseClientConnected(EndPoint remoteEndpoint);

        void RaiseClientDisconnected(EndPoint remoteEndpoint);

        void RaiseClientAccepted(EndPoint endpoint);

        void RaiseClientRejected(EndPoint remoteEndpoint);

        void RaiseMessageReceived(Message message);
    }
}
