using System.Net.Security;

namespace RoyalSoft.Network.Tcp.Client.Configuration.Interfaces
{
    public interface IEvents
    {
        /// <summary>
        /// When Ssl is enabled this event will be raised to validate remote certificate
        /// </summary>
        event RemoteCertificateValidationCallback OnRemoteCertificateValidation;

        /// <summary>
        /// When Ssl is enabled this event will be raised to validate local certificate
        /// </summary>
        event LocalCertificateSelectionCallback OnLocalCertificateValidation;
    }
}
