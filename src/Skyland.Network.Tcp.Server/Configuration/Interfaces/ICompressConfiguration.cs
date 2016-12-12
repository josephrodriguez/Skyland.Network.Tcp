#region using

using Skyland.Network.Core.Compression;

#endregion

namespace Skyland.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ICompressConfiguration
    {
        bool Enabled { get; }
        ICompressor Compressor { get; set; }
    }
}
