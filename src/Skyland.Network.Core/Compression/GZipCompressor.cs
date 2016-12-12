#region using

using System.IO;
using System.IO.Compression;

#endregion

namespace Skyland.Network.Core.Compression
{
    public class GZipCompressor : ICompressor
    {
        public byte[] Compress(byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                gzipStream.Write(data, 0, data.Length);
                gzipStream.Close();

                return memoryStream.ToArray();
            }
        }

        public byte[] Decompress(byte[] data)
        {
            var stream = new MemoryStream();

            using (var memoryStream = new MemoryStream(data))
            using (var decompressor = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                decompressor.CopyTo(stream);
                decompressor.Close();
            }

            stream.Position = 0;

            return stream.ToArray();
        }
    }
}
