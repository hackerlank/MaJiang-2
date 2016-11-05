﻿using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace MaJiang.WebSocket.Core.Net
{
  /// <summary>
  /// Stores the parameters used to configure a <see cref="SslStream"/> instance as a client.
  /// </summary>
  public class ClientSslConfiguration : SslConfiguration
  {
    #region Private Fields

    private X509CertificateCollection _certs;
    private string                    _host;

    #endregion

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientSslConfiguration"/> class with
    /// the specified <paramref name="targetHost"/>.
    /// </summary>
    /// <param name="targetHost">
    /// A <see cref="string"/> that represents the name of the server that shares
    /// a secure connection.
    /// </param>
    public ClientSslConfiguration (string targetHost)
      : this (targetHost, null, SslProtocols.Default, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientSslConfiguration"/> class with
    /// the specified <paramref name="targetHost"/>, <paramref name="clientCertificates"/>,
    /// <paramref name="enabledSslProtocols"/>, and <paramref name="checkCertificateRevocation"/>.
    /// </summary>
    /// <param name="targetHost">
    /// A <see cref="string"/> that represents the name of the server that shares
    /// a secure connection.
    /// </param>
    /// <param name="clientCertificates">
    /// A <see cref="X509CertificateCollection"/> that contains client certificates.
    /// </param>
    /// <param name="enabledSslProtocols">
    /// The <see cref="SslProtocols"/> enum value that represents the protocols used for
    /// authentication.
    /// </param>
    /// <param name="checkCertificateRevocation">
    /// <c>true</c> if the certificate revocation list is checked during authentication;
    /// otherwise, <c>false</c>.
    /// </param>
    public ClientSslConfiguration (
      string targetHost,
      X509CertificateCollection clientCertificates,
      SslProtocols enabledSslProtocols,
      bool checkCertificateRevocation)
      : base (enabledSslProtocols, checkCertificateRevocation)
    {
      _host = targetHost;
      _certs = clientCertificates;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the collection that contains client certificates.
    /// </summary>
    /// <value>
    /// A <see cref="X509CertificateCollection"/> that contains client certificates.
    /// </value>
    public X509CertificateCollection ClientCertificates {
      get {
        return _certs;
      }

      set {
        _certs = value;
      }
    }

    /// <summary>
    /// Gets or sets the callback used to select a client certificate to supply to the server.
    /// </summary>
    /// <remarks>
    /// If this callback returns <see langword="null"/>, no client certificate will be supplied.
    /// </remarks>
    /// <value>
    /// A <see cref="LocalCertificateSelectionCallback"/> delegate that references the method
    /// used to select the client certificate. The default value is a function that only returns
    /// <see langword="null"/>.
    /// </value>
    public LocalCertificateSelectionCallback ClientCertificateSelectionCallback {
      get {
        return CertificateSelectionCallback;
      }

      set {
        CertificateSelectionCallback = value;
      }
    }

    /// <summary>
    /// Gets or sets the callback used to validate the certificate supplied by the server.
    /// </summary>
    /// <remarks>
    /// If this callback returns <c>true</c>, the server certificate will be valid.
    /// </remarks>
    /// <value>
    /// A <see cref="RemoteCertificateValidationCallback"/> delegate that references the method
    /// used to validate the server certificate. The default value is a function that only returns
    /// <c>true</c>.
    /// </value>
    public RemoteCertificateValidationCallback ServerCertificateValidationCallback {
      get {
        return CertificateValidationCallback;
      }

      set {
        CertificateValidationCallback = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the server that shares a secure connection.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> that represents the name of the server that shares
    /// a secure connection.
    /// </value>
    public string TargetHost {
      get {
        return _host;
      }

      set {
        _host = value;
      }
    }

    #endregion
  }
}
