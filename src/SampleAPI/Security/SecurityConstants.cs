using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAPI.Security
{
    public static class SecurityConstants
    {
        public static IEnumerable<string> TrustedTokenIssuer = new List<string> { "C1677FBE7BDD6B131745E900E3B6764B4895A226" }; // Add Unique id for each callling application

        public static IEnumerable<string> TrustedAudienceToken = new List<string> { "urn:webapi:das" }; // Add Unique token for each calling application

    }
}