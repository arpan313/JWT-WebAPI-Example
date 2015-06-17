# JWT-WebAPI-Example
The example shows how we can use JWT token for authentication in webapi.To get more information on JWT please read
https://en.wikipedia.org/wiki/JSON_Web_Token


Example to generate JWT token-

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


