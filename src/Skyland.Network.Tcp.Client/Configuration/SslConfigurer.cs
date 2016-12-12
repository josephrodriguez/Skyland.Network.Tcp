#region using

using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace Skyland.Network.Tcp.Client.Configuration
{
    public class SslConfigurer
    {
        public SslConfigurer Certificate(X509CertificateCollection certificates)
        {
            return this;
        }

        public SslConfigurer Protocols(SslProtocols protocols)
        {
            return this;
        }

        public SslConfigurer CheckCertificateRevocation(bool check)
        {
            return this;
        }
    }
}
