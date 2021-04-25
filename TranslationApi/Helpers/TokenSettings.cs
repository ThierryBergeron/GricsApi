using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Helpers
{
    public class TokenSettings
    {
        public string JwtSecret { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
