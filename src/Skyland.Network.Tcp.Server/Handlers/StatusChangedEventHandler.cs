#region using

using System.Net;
using Skyland.Network.Tcp.Server.Enum;

#endregion

namespace Skyland.Network.Tcp.Server.Handlers
{
    public delegate void StatusChangedEventHandler(EndPoint endpoint, ConnectionState status);
}
