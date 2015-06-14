using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IoC.InjectModels;

namespace WebService
{
    public class WebServiceInjectModel:InjectModule
    {
        public override void Load()
        {
            Bind<IValidateCertificates>().To<BasicCertificateValidator>();
            base.Load();
        }
    }
}