#region using

using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace Skyland.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ISslConfiguration
    {
        bool Enabled { get; }
        SslProtocols Protocols { get; }
        X509Certificate Certificate { get; }
        bool CheckCertificateRevocation { get; }
        bool ClientCertificateIsRequired { get; }
        EncryptionPolicy EncryptionPolicy { get; }
    }
}
