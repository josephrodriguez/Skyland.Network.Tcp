#region using

using Skyland.Network.Core.Compression;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace Skyland.Network.Tcp.Server.Internal.Configuration
{
    internal class Compression : ICompressConfiguration
    {
        public bool Enabled { get; set; }
        public ICompressor Compressor { get; set; }
    }
}
