#region using

using System;

#endregion

namespace Skyland.Network.Tcp.Client.Configuration
{
    public class Configurer
    {
        public Configurer Compression(Action<CompressionConfigurer> action)
        {
            return this;
        }

        public Configurer Ssl(Action<SslConfigurer> sslAction)
        {
            return this;
        }
    }
}
