using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SimpleApi.Helpers;
using SimpleApi.Models;
using SimpleApi.Models.Auth;
using SimpleApi.Models.Firestore;

namespace SimpleApi.Services
{
    public class AuthService : IAuthService
    {

        private readonly TokenSettings _tokenSettings;
        private readonly IUserRepository _userRepository;
        private readonly byte[] _secret;

        public AuthService(TokenSettings tokenSettings, IUserRepository userRepository)
        {
            _tokenSettings = tokenSettings;
            _userRepository = userRepository;
            _secret = Encoding.ASCII.GetBytes(tokenSettings.JwtSecret);
        }

        async public Task<AuthenticationResponse> AuthenticateWithEmailAndPasswordAsync(AuthenticationRequest model)
        {
            var user = await _userRepository.GetUserWithNormalizedEmailAsync(model.Email.ToLower());
            var response = new AuthenticationResponse();

            // user not found
            if (user is null)
            {
                response.Error = "User not found";
                response.Authenticated = false;
                return response;
            }

            if (PasswordIsNotValid(model.Password, user.Password))
            {
                response.Error = "Invalid password";
                response.Authenticated = false;
                return response;
            }

            var token = GenerateJwtToken(user);
            response.Name = user.Name;
            response.Email = model.Email;
            response.Token = token;
            response.Authenticated = true;
            return response;
        }

        private string GenerateJwtToken(FirestoreUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.AccessTokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var principal = new JwtSecurityTokenHandler().ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _tokenSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    ValidAudience = _tokenSettings.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                }, out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }


        private bool PasswordIsNotValid(string password, string hash)
        {
            // would be hash in prod
            return password != hash;
        }

        async public Task<User> CreateUserAsync(FirestoreUser firestoreUser)
        {
            return await _userRepository.AddAsync(firestoreUser);
        }
    }
}
