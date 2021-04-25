using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models.Auth
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
        public bool Authenticated { get; set; }
    }
}
