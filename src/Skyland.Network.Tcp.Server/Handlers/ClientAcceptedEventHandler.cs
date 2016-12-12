#region using

using System.Net;

#endregion

namespace Skyland.Network.Tcp.Server.Handlers
{
    public delegate void ClientAcceptedEventHandler(EndPoint endpoint);
}
