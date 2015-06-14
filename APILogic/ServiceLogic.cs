using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace APILogic
{
    /// <summary>
    /// Логика работы с WebAPI
    /// </summary>
    public class ServiceLogic
    {
        private readonly string _serviceApp = ConfigurationManager.AppSettings["serviceApplication"];

        /// <summary>
        /// Отправляем запрос на регистрацию сертификата
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendRequestQuery(int idCertificate)
        {
            var httpClient = new HttpClient();
            var content =  new FormUrlEncodedContent(new List<KeyValuePair<string,string>>
            {
                new KeyValuePair<string, string>("idCertificate", idCertificate.ToString())
            });
            HttpResponseMessage result = await httpClient.PostAsync(String.Format("http://{0}/api/Request", _serviceApp), content);

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Отправляем основной запрос к защищенному контроллеру
        /// </summary>
        /// <param name="cert"></param>
        /// <returns></returns>
        public async Task<string> SendMainQuery(X509Certificate2 cert)
        {
            var messageHandler = new WebRequestHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true)
            };

            messageHandler.ClientCertificates.Add(cert);

            var httpClient = new HttpClient(messageHandler);
            var result = await httpClient.GetAsync(String.Format("https://{0}/api/Values", _serviceApp));
            return await result.Content.ReadAsStringAsync();
        }
    }
}
