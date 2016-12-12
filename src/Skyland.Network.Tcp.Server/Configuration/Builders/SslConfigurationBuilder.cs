#region using

using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;
using Skyland.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace Skyland.Network.Tcp.Server.Configuration.Builders
{
    public class SslConfigurationBuilder : Builder<ISslConfiguration>
    {
        private X509Certificate _certificate;
        private SslProtocols _protocols;
        private EncryptionPolicy _encryptionPolicy;
        private bool _checkCertificateRevocation, _clientCertificateIsRequired, _enabled;


        public SslConfigurationBuilder Certificate(string filename)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            if (_certificate != null)
                throw new Exception();

            _certificate = new X509Certificate(filename);
            _enabled = true;

            return this;
        }

        public SslConfigurationBuilder Certificate(string filename, string password, X509KeyStorageFlags flags = X509KeyStorageFlags.DefaultKeySet)
        {
            if(filename == null)
                throw new ArgumentNullException("filename");

            if (_certificate != null)
                throw new Exception();

            _certificate = new X509Certificate(filename, password, flags);
            _enabled = true;

            return this;
        }

        public SslConfigurationBuilder Protocols(SslProtocols protocols)
        {
            _enabled = true;

            _protocols = protocols;
            return this;
        }

        public SslConfigurationBuilder CheckCertificateRevocation()
        {
            _enabled = true;
            _checkCertificateRevocation = true;
            return this;
        }

        public SslConfigurationBuilder ClientCertificateRequired()
        {
            _enabled = true;
            _clientCertificateIsRequired = true;
            return this;
        }

        public SslConfigurationBuilder Policy(EncryptionPolicy policy)
        {
            _enabled = true;
            _encryptionPolicy = policy;
            return this;
        }

        internal override ISslConfiguration Build()
        {
            return 
                new SslConfiguration
                {
                    Enabled = _enabled,
                    Certificate = _certificate,
                    Protocols = _protocols == SslProtocols.None ? SslProtocols.Default : _protocols,
                    EncryptionPolicy = _encryptionPolicy,
                    CheckCertificateRevocation = _checkCertificateRevocation,
                    ClientCertificateIsRequired = _clientCertificateIsRequired
                };
        }
    }
}
