#region using

using Skyland.Network.Core.Compression;

#endregion

namespace Skyland.Network.Tcp.Client.Configuration
{
    public class CompressionConfigurer
    {
        public CompressionConfigurer Use<T>() where T : ICompressor
        {
            return this;
        }
    }
}
