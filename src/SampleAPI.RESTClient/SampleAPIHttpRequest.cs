using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using DAS.RESTClient;


namespace SampleAPI.RESTClient
{
    public class SampleAPIHttpRequest
    {
        public async Task<HttpResponseMessage> SendApiRequestAsync(HttpRequestMessage request, string token, object contents = null)
        {
            var client = SampleHttpClientFactory.Get();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (contents != null)
            {
                request.Content = new ObjectContent(contents.GetType(), contents, new JsonMediaTypeFormatter());

            }

            return await client.SendAsync(request);
        }
        public HttpResponseMessage SendApiRequest(HttpRequestMessage request, string token, object contents = null)
        {
            var client = SampleHttpClientFactory.Get();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (contents != null)
            {
                request.Content = new ObjectContent(contents.GetType(), contents, new JsonMediaTypeFormatter());

            }

            return client.SendAsync(request).Result;
        }
    }
}
