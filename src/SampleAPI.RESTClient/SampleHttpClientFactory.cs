using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace DAS.RESTClient
{
    public class SampleHttpClientFactory
    {
        private static readonly Lazy<HttpClient> Singleton = new Lazy<HttpClient>(BuildHttpClient);

        private static HttpClient BuildHttpClient()
        {
            var baseUrl = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"], UriKind.Absolute);
            return new HttpClient { BaseAddress = baseUrl };
        }

        public static HttpClient Get()
        {
            return Singleton.Value;
        }
    }
}
