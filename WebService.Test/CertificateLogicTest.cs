using System;
using System.Collections.Generic;
using APILogic;
using DAL.POCO;
using DAL.Repository;
using DAL.Specifications;
using IoC.IoCContainer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WebService.Test
{
    [TestClass]
    public class CertificateLogicTest
    {
        /// <summary>
        /// Пример unit-теста.
        /// Тестируем получение из БД сертификата для установки.
        /// </summary>
        [TestMethod]
        public void InstallCertificateTest()
        {
            var certificate = new UserCertificate()
            {
                Id = 1,
                IsInstalled = false,
                CreateDate = new DateTime(2015,1,1),
                Certificate = "TestCertificateInBase64",
                Password = "123456",
                UserName = "SirHally",
                Thumbprint = "TestThumbprint"
            };
            var certificateForCheck = new UserCertificate()
            {
                Id = 1,
                IsInstalled = false,
                CreateDate = new DateTime(2015, 1, 1),
                Certificate = "TestCertificateInBase64",
                Password = "123456",
                UserName = "SirHally",
                Thumbprint = "TestThumbprint"
            };

            var mockContext = new Mock<IEntity>();
            mockContext.Setup(t => t.SaveChanges());
            IoCContainer.Rebind(typeof(IEntity),mockContext.Object);

            var mockRepository = new Mock<IRepository<UserCertificate>>();
            mockRepository.Setup(t => t.Find(It.IsAny<ISpecification<UserCertificate>>())).Returns(new List<UserCertificate>
            {
               certificate
            });
            IoCContainer.Rebind(typeof(IRepository<UserCertificate>), mockRepository.Object);

            Mock<CertificateLogic> mock = new Mock<CertificateLogic>();
            mock.Setup(t => t.Install(It.IsAny<string>(), It.IsAny<string>()));
            mock.Object.InstallCertificate();


            Assert.IsTrue(certificate.IsInstalled);
            mockContext.Verify(t=>t.SaveChanges(),Times.Once);
            mockRepository.Verify(t => t.Find(It.Is<ISpecification<UserCertificate>>(q => q.IsSatisfiedBy(certificateForCheck))), Times.Once);
            mock.Verify(t => t.Install(It.Is<string>(q => q == certificate.Certificate), It.Is<string>(q => q == certificate.Password)), Times.Once);
            
        }
    }
}
