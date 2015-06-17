using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
namespace SampleAPI.RESTClient
{
    public class TokenWriter
    {
        public static string GenerateJwtTokenString(string auidenceToken, string issuer)
        {
            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(auidenceToken))
            {
                throw new Exception("Issuer or auidenceToken could not be null or empty");
            }

            var utcNow = DateTime.UtcNow;
            var expireTime = utcNow.Add(XmlConvert.ToTimeSpan("PT5M"));
            var signingCredentials = GetCredentials();         
            var token = new JwtSecurityToken(issuer, auidenceToken, null, utcNow, expireTime, signingCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private static SigningCredentials GetCredentials()
        {
           
            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            publicAndPrivate.FromXmlString("<RSAKeyValue><Modulus>pVjBig8y4aEnxSH/V0MR6g/73rEcmhyU5tSPieFx8cTTiVU9b6gKdPUon3DSdA+fJYS+CA0K7JSi0oofO4R15NuX63F+SdukBjXrEJXkByf3qQl7OA8dHVSr7ZCiPs/hPIddzjCQZExBUGnIXh7TsjVkVbDr8WxZ25Yyxk6CTsJVE1XS1cYGqm9G6UjobW00+tW6D1EY3vU1tXWE8rVBpssVLa8sinFMS6Lj7XVUabCESCT8dGyIvyAyu0eXKQksGuQe6BgyQdRm5qFipUE5kIkBvEvqBCSNSORzNvb7JCikX1KfRU1wQUQgNNlSLKLm2xRkKzeJvae/13chTRThFw==</Modulus><Exponent>AQAB</Exponent><P>21r2dom4iY8OwyE4Wk/3gNqfmEBq53SuOilCNRKvGuI3TBrNokSMsnLMWKXlkClLsw39kSl8EWfAyUbstE9dU50T79GiGuwng/QY4x4T2+C2tdKCwimSBHcEk2BDq2o19HyHYa6ctx7GKMbwSrkYpMFrAoREWpxPhunPDtmmhI8=</P><Q>wPgKAPtwUrP3w6aimyPmStCXRQuQHVbAtDAfJtbU2URnmFJjQvDWdt94UewXdkSo4sefA85GwEPjPWNfmX0f7w+ZaDuZXujJY0FgfqU3AHZNPJUaRaI93HsObPAXJIla/7IxLZ+HIQ1kouqx+kqyRMEEZ9Ofw/GeKxqiRtGP7vk=</Q><DP>hbpU2ztm52XxqABOrLBnwYSHG3jaM7UsqoSQum5jlk4nTBxjFPQZmojmD31EELk8TzLOTXsvCiVooMy+bcacrObsSp5Q53zlMkrUCVJC5R+ABCUMVnalcFdm0Lo1RwF/V0e7+U9Md08pm558y/FAFuFb7bLZQSxeR2+vLex+2f0=</DP><DQ>i34CRUl9ndtIvJ8lpn5iCC0XXKsKJ5tBvD+ZFUtxHAoN1Rehd49/iPjsElRXdYAWkbD56xdfH7czennubpb70aV2INoMeYZPpjdpWMN3qhbaqHOkRTgN/ebau7cnE5tTM8mNMUzDswnqeLatp5/9lCPvWlqpgEyWKNSdABKXOIk=</DQ><InverseQ>OxM/bRs7RJ6MXGDsP4otL+Ofkjj048s2OrlpB9OFCeHb+PhiTgzUV3aRPsCU25Mgy8Szi50xs1H4jTxJVb57i5aon1S8IFlYHlZEuEYqfJMH89ma3w77yPMuDteNbn/g6NMvLyubXZLYgleD0ZbHy0yR3fh2Fj/gOIh97P5RHAM=</InverseQ><D>L6S3f9SJUMJi90aokLhevRcF+FEIcM+ziB/PjGdNseCJI5VQMpA1EyZhIjAcNB0cOm9ZYnB/qVPa5tet0DSG0/8Cc3EsdpO3W6LFrO3D2twFXMvCmZChTwZK1BVM6tRzjkFjIjqUIJJob4dLmPNBBKSl2Js2IUabTc6lqsaMfazCj4kI2t2ujkV8tyU7UfWk3rBj5FU1S7rxbPLSMDfooPoawhyg6T09jqU72cN8lQC94Hq1dxJgEllBSVNe/C/Ef2GBQyc+bQHQlXd1aabXje9tT3Vk8d5UGmAXv6NYPpQ2zIpX91wg2lOGKpj+TbqO4bAH3IC47fOqcBfHaUjyQQ==</D></RSAKeyValue>");

            return new SigningCredentials(new RsaSecurityKey(publicAndPrivate), SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);
        }
    }
}
