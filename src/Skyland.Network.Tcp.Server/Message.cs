using System.Net;

namespace Skyland.Network.Tcp.Server
{
    public class Message
    {
        public EndPoint Endpoint { get; private set; }
        public byte[] Data { get; internal set; }

        internal Message(EndPoint endpoint, byte[] data)
        {
            Endpoint = endpoint;
            Data = data;
        }
    }
}
