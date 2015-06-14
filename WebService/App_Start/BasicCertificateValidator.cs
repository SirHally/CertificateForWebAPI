using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using DAL.POCO;
using DAL.Repository;
using IoC.IoCContainer;

namespace WebService
{
    public class BasicCertificateValidator : IValidateCertificates
    {
        public UserCertificate GetCertificate(X509Certificate2 certificate)
        {
            using (var context = IoCContainer.Get<IEntity>())
            {
                var repository = context.GetRepository<IRepository<UserCertificate>>();
                var cert = repository.Find(new DAL.Specifications.POCO.User.ByThumbprint(certificate.Thumbprint)).SingleOrDefault();
                return cert;
            }
        }

        public IPrincipal GetPrincipal(UserCertificate certificate2)
        {
            return new GenericPrincipal(
                new GenericIdentity(certificate2.UserName), new[] { "User" });
        }
    }
}