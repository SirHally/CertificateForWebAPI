using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using IoC.IoCContainer;

namespace WebService
{
    public class CertificateAuthHandler : DelegatingHandler
    {

        public IValidateCertificates CertificateValidator { get; set; }

        public CertificateAuthHandler()
        {
            CertificateValidator = IoCContainer.Get<IValidateCertificates>();
            InnerHandler = new HttpControllerDispatcher(GlobalConfiguration.Configuration); 
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage>
            SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            X509Certificate2 certificate = request.GetClientCertificate();
            if (certificate != null)
            {
                var userCertificate = CertificateValidator.GetCertificate(certificate);
                if (userCertificate != null)
                {
                    Thread.CurrentPrincipal = CertificateValidator.GetPrincipal(userCertificate);
                    return base.SendAsync(request, cancellationToken);
                }
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => request.CreateResponse(HttpStatusCode.Unauthorized), cancellationToken);
         
           
        }
    }
}