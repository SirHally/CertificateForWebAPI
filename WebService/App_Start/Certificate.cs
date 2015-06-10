using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebService.App_Start
{
    public interface IValidateCertificates
    {
        bool IsValid(X509Certificate2 certificate);
        IPrincipal GetPrincipal(X509Certificate2 certificate2);
    }

    public class BasicCertificateValidator : IValidateCertificates
    {
        public bool IsValid(X509Certificate2 certificate)
        {
            return true;
            return certificate.Issuer == "CN=Awesome CA"
                   && certificate.GetCertHashString() == "B04AED3DA6CB4BD2F817EE2C726183C00035F4C6";
            //make a better check here (eg. against mapping, verify the chain etc)
        }

        public IPrincipal GetPrincipal(X509Certificate2 certificate2)
        {
            return new GenericPrincipal(
                new GenericIdentity(certificate2.Subject), new[] { "User" });
        }
    }

    public class CertificateAuthHandler : DelegatingHandler
    {
        public IValidateCertificates CertificateValidator { get; set; }

        public CertificateAuthHandler()
        {
            CertificateValidator = new BasicCertificateValidator();
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage>
            SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            X509Certificate2 certificate = request.GetClientCertificate();
             if (certificate == null || !CertificateValidator.IsValid(certificate))
            {
                return Task<HttpResponseMessage>.Factory.StartNew(
                    () => request.CreateResponse(HttpStatusCode.Unauthorized));

            }
            Thread.CurrentPrincipal = CertificateValidator.GetPrincipal(certificate);
            return base.SendAsync(request, cancellationToken);
        }
    }
}