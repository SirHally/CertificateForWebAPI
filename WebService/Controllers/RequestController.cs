using System.Web.Http;
using APILogic;
using IoC.IoCContainer;
using WebService.ViewModel;

namespace WebService.Controllers
{
    /// <summary>
    /// Контроллер регистрации новых сертификатов
    /// </summary>
    public class RequestController : ApiController
    {
        private readonly CertificateLogic _logic = IoCContainer.Get<CertificateLogic>();

        // POST api/request
        /// <summary>
        /// Регистрируем новый сертификат
        /// </summary>
        public void Post([FromBody]RequestPostData data)
        {
            _logic.InstallCertificate(data.IdCertificate);
        }
    }

}
