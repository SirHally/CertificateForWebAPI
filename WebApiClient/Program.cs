using System;
using System.Threading.Tasks;
using APILogic;
using IoC.InjectModels;
using IoC.IoCContainer;

namespace WebApiClient
{
    class Program
    {
        /// <summary>
        /// Логика управления сертификатами
        /// </summary>
        private static readonly CertificateLogic CertificateLogic  = new CertificateLogic();

        /// <summary>
        /// Логика работы с WebAPI
        /// </summary>
        private static readonly ServiceLogic ServiceLogic = new ServiceLogic();


        static void Main(string[] args)
        {
            IoCContainer.Load(new InjectModule());
            while (true)
            {
                var result = MakeRequest();
                result.Wait();
            }
        }

        /// <summary>
        /// Делает запрос на защищенный WebAPI-ресурс
        /// </summary>
        private async static Task MakeRequest()
        {
            Console.WriteLine("Введите имя пользователя");
            string userName = Console.ReadLine();
            if (String.IsNullOrEmpty(userName))
            {
                Console.WriteLine("Необходимо ввести имя пользователя");
            }
            else
            {
                CreateSelfSignResult result = CertificateLogic.CreateSelfSign(userName);
                bool request = await ServiceLogic.SendRequestQuery(result.Id);
                if (request)
                {
                    string resultQuery = await ServiceLogic.SendMainQuery(result.Certificate);
                    Console.WriteLine(resultQuery);
                }
                else
                {
                    Console.WriteLine("Ошибка при регистрации сертификата");
                }
            }
            Console.ReadLine();
        }

    }
}
