using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DAL.POCO;
using DAL.Repository;
using IoC.IoCContainer;

namespace APILogic
{
    /// <summary>
    /// Логика работы с сертификатами
    /// </summary>
    public class CertificateLogic
    {
        /// <summary>
        /// Дата начала действия сертификата
        /// </summary>
        private readonly DateTime _certificateDateStart = new DateTime(2015, 1, 1);

        /// <summary>
        /// Дата окончания действия сертификата
        /// </summary>
        private readonly DateTime _certificateDateEnd = new DateTime(2016, 1, 1);

        /// <summary>
        /// Имя сертификата
        /// </summary>
        private const string X500 = "CN=localhost";

        /// <summary>
        /// Создаем новый самоподписанный сертификат
        /// </summary>
        /// <returns></returns>
        public CreateSelfSignResult CreateSelfSign(string userName)
        {
            string password = Guid.NewGuid().ToString();

            byte[] certificateBytes = Certificate.CreateSelfSignCertificatePfx(X500, _certificateDateStart, _certificateDateEnd, password);

            var cert = new X509Certificate2(certificateBytes, password);

            var id = SaveToDatabase(certificateBytes, cert, password, userName);

            return new CreateSelfSignResult()
            {
                Certificate = cert,
                Id = id
            };
        }

        /// <summary>
        ///     Сохраняем информацию о сертификате в БД
        /// </summary>
        public int SaveToDatabase(byte[] certificateByte, X509Certificate2 certificate, string password, string userName)
        {
            using (var context = IoCContainer.Get<IEntity>())
            {
                var repository = context.GetRepository<IRepository<UserCertificate>>();
                var newItem = new UserCertificate
                {
                    Certificate = Convert.ToBase64String(certificateByte),
                    CreateDate = DateTime.Now,
                    Thumbprint = certificate.Thumbprint,
                    IsInstalled = false,
                    Password = password,
                    UserName = userName
                };
                repository.Add(newItem);
                context.SaveChanges();
                return newItem.Id;
            }
        }

        /// <summary>
        /// Устанавливаем все неустановленные сертификаты
        /// </summary>
        public void InstallCertificate()
        {
            using (var context = IoCContainer.Get<IEntity>())
            {
                var repository = context.GetRepository<IRepository<UserCertificate>>();
                IEnumerable<UserCertificate> certs = repository.Find(new DAL.Specifications.POCO.User.ByIsInstalled(false)).ToList();

                foreach (UserCertificate certificate in certs)
                {
                    Install(certificate.Certificate, certificate.Password);
                    certificate.IsInstalled = true;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Устанавливаем сертификат по Id
        /// </summary>
        /// <param name="idCertificate"></param>
        public void InstallCertificate(int idCertificate)
        {
            using (var context = IoCContainer.Get<IEntity>())
            {
                var repository = context.GetRepository<IRepository<UserCertificate>>();
                UserCertificate cert = repository.Find(new DAL.Specifications.POCO.User.ById(idCertificate)).SingleOrDefault();
                if (cert != null)
                {
                    Install(cert.Certificate, cert.Password);
                    cert.IsInstalled = true;
                   
                    context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Устанавливаем сертификат в доверенные лица текущего компьютера.
        /// </summary>
        /// <param name="base64Certificate"></param>
        /// <param name="password"></param>
        public virtual void Install(string base64Certificate, string password)
        {
            byte[] certByte = Convert.FromBase64String(base64Certificate);
            var store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite);
            try
            {
                var cert = new X509Certificate2(certByte, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                store.Add(cert);
            }
            finally
            {
                store.Close();
            }
        }


        /// <summary>
        ///     Сохраняем сертификат в PFX
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="certificate"></param>
        public void SaveToFile(string fileName, byte[] certificate)
        {
            using (var fs = File.Create(fileName))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    writer.Write(certificate);
                }
            }
        }
    }
}