using DAS.RESTClient;
using SampleAPI.RESTClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAS.TestRESTClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleApiCommunication = new SampleApiCommunication();
            var response = sampleApiCommunication.GetWebApiRepsonse();
            Console.WriteLine(response);
            Console.ReadLine();
        }
    }
}
