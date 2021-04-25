using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SimpleApi.Models;
using SimpleApi.Models.Auth;

namespace SimpleApi.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> AuthenticateWithEmailAndPasswordAsync(AuthenticationRequest model);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
        Task<User> CreateUserAsync(FirestoreUser user);
    }
        
}
