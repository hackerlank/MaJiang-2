using System.Net.Security;
using System.Security.Authentication;

namespace MaJiang.WebSocket.Core.Net
{
  /// <summary>
  /// Stores the parameters used to configure a <see cref="SslStream"/> instance.
  /// </summary>
  /// <remarks>
  /// The SslConfiguration class is an abstract class.
  /// </remarks>
  public abstract class SslConfiguration
  {
    #region Private Fields

    private LocalCertificateSelectionCallback   _certSelectionCallback;
    private RemoteCertificateValidationCallback _certValidationCallback;
    private bool                                _checkCertRevocation;
    private SslProtocols                        _enabledProtocols;

    #endregion

    #region Protected Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SslConfiguration"/> class with
    /// the specified <paramref name="enabledSslProtocols"/> and
    /// <paramref name="checkCertificateRevocation"/>.
    /// </summary>
    /// <param name="enabledSslProtocols">
    /// The <see cref="SslProtocols"/> enum value that represents the protocols used for
    /// authentication.
    /// </param>
    /// <param name="checkCertificateRevocation">
    /// <c>true</c> if the certificate revocation list is checked during authentication;
    /// otherwise, <c>false</c>.
    /// </param>
    protected SslConfiguration (SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
    {
      _enabledProtocols = enabledSslProtocols;
      _checkCertRevocation = checkCertificateRevocation;
    }

    #endregion

    #region Protected Properties

    /// <summary>
    /// Gets or sets the callback used to select a certificate to supply to the remote party.
    /// </summary>
    /// <remarks>
    /// If this callback returns <see langword="null"/>, no certificate will be supplied.
    /// </remarks>
    /// <value>
    /// A <see cref="LocalCertificateSelectionCallback"/> delegate that references the method
    /// used to select a certificate. The default value is a function that only returns
    /// <see langword="null"/>.
    /// </value>
    protected LocalCertificateSelectionCallback CertificateSelectionCallback {
      get {
        return _certSelectionCallback ??
               (_certSelectionCallback =
                 (sender, targetHost, localCertificates, remoteCertificate, acceptableIssuers) =>
                   null);
      }

      set {
        _certSelectionCallback = value;
      }
    }

    /// <summary>
    /// Gets or sets the callback used to validate the certificate supplied by the remote party.
    /// </summary>
    /// <remarks>
    /// If this callback returns <c>true</c>, the certificate will be valid.
    /// </remarks>
    /// <value>
    /// A <see cref="RemoteCertificateValidationCallback"/> delegate that references the method
    /// used to validate the certificate. The default value is a function that only returns
    /// <c>true</c>.
    /// </value>
    protected RemoteCertificateValidationCallback CertificateValidationCallback {
      get {
        return _certValidationCallback ??
               (_certValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true);
      }

      set {
        _certValidationCallback = value;
      }
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets a value indicating whether the certificate revocation list is checked
    /// during authentication.
    /// </summary>
    /// <value>
    /// <c>true</c> if the certificate revocation list is checked; otherwise, <c>false</c>.
    /// </value>
    public bool CheckCertificateRevocation {
      get {
        return _checkCertRevocation;
      }

      set {
        _checkCertRevocation = value;
      }
    }

    /// <summary>
    /// Gets or sets the SSL protocols used for authentication.
    /// </summary>
    /// <value>
    /// The <see cref="SslProtocols"/> enum value that represents the protocols used for
    /// authentication.
    /// </value>
    public SslProtocols EnabledSslProtocols {
      get {
        return _enabledProtocols;
      }

      set {
        _enabledProtocols = value;
      }
    }

    #endregion
  }
}
