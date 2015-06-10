using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptographyLogic;

namespace WebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var logic = new CertificateLogic();
            logic.GenerateCertificate();
            Console.ReadLine();
        }
    }
}
