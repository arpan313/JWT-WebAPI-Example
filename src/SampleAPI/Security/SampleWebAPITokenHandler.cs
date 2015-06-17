using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.ServiceModel.Security.Tokens;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SampleAPI.Security
{
    public class SampleWebAPITokenHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string jwtToken;
            if (!TryRetrieveToken(request, out jwtToken))
            {
                return BuildResponseErrorMessage(request, HttpStatusCode.Unauthorized, "Invalid security token.");
            }

            try
            {
                var identity= ValidateJwtToken(jwtToken);                                    
                return await base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                return BuildResponseErrorMessage(request, HttpStatusCode.Unauthorized, "Invalid security token.");
            }
            catch (Exception)
            {
                return BuildResponseErrorMessage(request, HttpStatusCode.Unauthorized, "Invalid security token.");
            }
        }

        private HttpResponseMessage BuildResponseErrorMessage(HttpRequestMessage request, HttpStatusCode statusCode, string errorMessage)
        {
            var response = request.CreateResponse(statusCode, errorMessage);
            return response;
        }

        // Reads the token from the authorization header on the incoming request
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            if (!request.Headers.Contains("Authorization")) return false;
            var authzHeader = request.Headers.GetValues("Authorization").First<string>();

            // Verify Authorization header contains 'Bearer' scheme
            token = authzHeader.StartsWith("Bearer ") ? authzHeader.Split(' ')[1] : null;

            return null != token;
        }
        public static ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            RSACryptoServiceProvider publicOnly = new RSACryptoServiceProvider();
            //TODO: Need to put the public key in database
            publicOnly.FromXmlString("<RSAKeyValue><Modulus>pVjBig8y4aEnxSH/V0MR6g/73rEcmhyU5tSPieFx8cTTiVU9b6gKdPUon3DSdA+fJYS+CA0K7JSi0oofO4R15NuX63F+SdukBjXrEJXkByf3qQl7OA8dHVSr7ZCiPs/hPIddzjCQZExBUGnIXh7TsjVkVbDr8WxZ25Yyxk6CTsJVE1XS1cYGqm9G6UjobW00+tW6D1EY3vU1tXWE8rVBpssVLa8sinFMS6Lj7XVUabCESCT8dGyIvyAyu0eXKQksGuQe6BgyQdRm5qFipUE5kIkBvEvqBCSNSORzNvb7JCikX1KfRU1wQUQgNNlSLKLm2xRkKzeJvae/13chTRThFw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
            var issuesSigningTokenCol = new List<SecurityToken>();
            issuesSigningTokenCol.Add(new RsaSecurityToken(publicOnly));

            var validationParams =
                new TokenValidationParameters()
                {
                    ValidAudiences = SecurityConstants.TrustedAudienceToken,
                    ValidIssuers = SecurityConstants.TrustedTokenIssuer,
                    ValidateIssuer = true,                   
                   IssuerSigningTokens=issuesSigningTokenCol,
                    RequireExpirationTime = true,
                    
                };

            SecurityToken outputSecurityToken = null;
            var principle = tokenHandler.ValidateToken(jwtToken, validationParams, out outputSecurityToken);
            return principle;

        }
    }
}