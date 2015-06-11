using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CryptographyLogic
{
    public class CertificateLogic
    {
        public async void GenerateCertificate()
        {
            // byte[] certificate = Certificate.CreateSelfSignCertificatePfx("cn=localhost", new DateTime(2015, 1, 1), new DateTime(2016, 1, 1), "123456");
            //      var mKeyDataPfx = Convert.ToBase64String(certificate);
            /*  using (FileStream fs = File.Create(@"C:\a.pfx"))
        
            {
                using (var writer = new BinaryWriter(fs))
                {
                    writer.Write(certificate);
                }
            }
            return;*/
            //  var cert = new X509Certificate2(certificate,"123456");

            X509Certificate2Collection cert12 = GetCertificateFromStore();

            var messageHandler = new WebRequestHandler();
            messageHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            messageHandler.UseDefaultCredentials = false;

            //Trust all certificates
            ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
/*
// trust sender
            ServicePointManager.ServerCertificateValidationCallback
                = ((sender, cert, chain, errors) => cert.Subject.Contains("YourServerName"));

// validate cert by calling a function
            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;

            */
            foreach (X509Certificate2 certificate2 in cert12)
            {
       //         messageHandler.ClientCertificates.Add(certificate2);
            }
            messageHandler.ClientCertificates.Add(cert12[6]);
          //  messageHandler.ClientCertificates.Add(cert12[1]);

            var httpClient = new HttpClient(messageHandler);
            var result = await httpClient.GetAsync("https://localhost/WebService/api/Values");
            var response = await result.Content.ReadAsStringAsync();
            Console.WriteLine(response);
        }

        // callback used to validate the certificate in an SSL conversation
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors policyErrors)
        {
            return true;
            var result = false;
            if (cert.Subject.ToUpper().Contains("YourServerName"))
            {
                result = true;
            }

            return result;
        }

        private static X509Certificate2Collection GetCertificateFromStore()
        {
            // Get the certificate store for the current user.
            var store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                var currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                // X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, "CN=localhost", false);
                // if (signingCert.Count == 0)
                //     return null;
                // Return the first certificate in the collection, has the right name and is current.
                return currentCerts;
            }
            finally
            {
                store.Close();
            }
        }
    }
}