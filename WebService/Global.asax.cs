using System.Web.Http;
using System.Web.Mvc;
using IoC.IoCContainer;

namespace WebService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            IoCContainer.Load(new WebServiceInjectModel());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            

        }
    }
}