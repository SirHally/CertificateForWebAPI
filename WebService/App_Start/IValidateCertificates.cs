using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using DAL.POCO;

namespace WebService
{
    /// <summary>
    /// ��������� ������������ � ���������� �������
    /// </summary>
    public interface IValidateCertificates
    {
        UserCertificate GetCertificate(X509Certificate2 certificate);
        IPrincipal GetPrincipal(UserCertificate certificate2);
    }
}