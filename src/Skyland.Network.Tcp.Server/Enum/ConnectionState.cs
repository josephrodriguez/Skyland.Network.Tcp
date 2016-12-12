using System;

namespace Skyland.Network.Tcp.Server.Enum
{
    [Flags]
    public enum ConnectionState
    {
        Open    = 0x1,
        Closed  = 0x2,
        Unknow  = 0x4
    }
}
