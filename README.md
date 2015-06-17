# JWT-WebAPI-Example
The example shows how we can use JWT token for authentication in webapi.To get more information on JWT please read
https://en.wikipedia.org/wiki/JSON_Web_Token


Example to generate JWT token-


            var utcNow = DateTime.UtcNow;
            var expireTime = utcNow.Add(XmlConvert.ToTimeSpan("PT5M"));
            var signingCredentials = GetCredentials();         
            var token = new JwtSecurityToken(issuer, auidenceToken, null, utcNow, expireTime, signingCredentials);




