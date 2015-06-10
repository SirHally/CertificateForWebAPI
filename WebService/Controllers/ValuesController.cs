using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class ValuesController : ApiController
    {
        [RequireHttps]
        public string Get()
        {
            return "string";
        }


    }
}