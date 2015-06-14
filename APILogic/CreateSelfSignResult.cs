using System.Security.Cryptography.X509Certificates;

namespace APILogic
{
    public struct CreateSelfSignResult
    {
        public int Id { get; set; }

        public X509Certificate2 Certificate { get; set; }
    }
}