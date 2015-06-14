using System.Web.Http;

namespace WebService.Controllers
{
    public class ValuesController : ApiController
    {
        public string Get()
        {
            return "Hello from WebAPI!";
        }
    }
}