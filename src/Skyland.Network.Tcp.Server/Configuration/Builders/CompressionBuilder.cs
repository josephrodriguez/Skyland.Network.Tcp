#region using

using System;
using Skyland.Network.Core.Compression;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;
using Skyland.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace Skyland.Network.Tcp.Server.Configuration.Builders
{
    public class CompressionBuilder : Builder<ICompressConfiguration>
    {
        private bool _enabled;
        private ICompressor _compressor;

        public CompressionBuilder Gzip()
        {
            if(_compressor != null)
                throw new Exception();

            _compressor = new GZipCompressor();
            _enabled = true;

            return this;
        }

        internal override ICompressConfiguration Build()
        {
            return 
                new Compression
                {
                    Enabled = _enabled,
                    Compressor = _compressor
                };
        }
    }
}
