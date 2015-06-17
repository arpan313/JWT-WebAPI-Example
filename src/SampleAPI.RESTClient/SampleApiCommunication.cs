using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.RESTClient
{
    public class SampleApiCommunication
    {
        private const string SampleWebApiUrl = "api/test";
        private SampleAPIHttpRequest _sampleAPIHttpRequest;
        public SampleApiCommunication()
        {
            _sampleAPIHttpRequest = new SampleAPIHttpRequest();
        }

        public HttpResponseMessage SendWebApiRequest(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url, UriKind.Relative));
            var jwtToken = TokenWriter.GenerateJwtTokenString(SecurityConstant.SampleAPIAuidence, SecurityConstant.IssuerId);

            var response = _sampleAPIHttpRequest.SendApiRequest(request, jwtToken);
            return response;
        }

        public HttpResponseMessage SendWebApiWithContentRequest(string url, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url, UriKind.Relative));
            var jwtToken = TokenWriter.GenerateJwtTokenString(SecurityConstant.SampleAPIAuidence, SecurityConstant.IssuerId);

            var response = _sampleAPIHttpRequest.SendApiRequest(request, jwtToken, content);
            return response;
        }
        public object GetWebApiRepsonse()
        {
            var response = SendWebApiRequest(SampleWebApiUrl);
            if (response.IsSuccessStatusCode)
                return "Authentication Successfully";
            else
                return "Error";
        }
    }
}
