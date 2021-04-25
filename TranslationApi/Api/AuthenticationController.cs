using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleApi.Models;
using SimpleApi.Models.Firestore;
using SimpleApi.Services;

namespace SimpleApi.Api
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public AuthenticationController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        async public Task<IActionResult> PostUser(AuthenticationRequest authRequest)
        {
            try
            {
                var authResponse = await _authService.AuthenticateWithEmailAndPasswordAsync(authRequest);

                // if authenticated
                if (authResponse.Authenticated)
                {
                    return Ok(authResponse);
                }

                return Unauthorized();
            }
            catch
            {
                return StatusCode(500, "Internal servor error");
            }
        }

        [Authorize]
        [HttpPost("createuser")]
        async public Task<IActionResult> CreateUser(FirestoreUser user)
        {
            try
            {
                var newUser = await _userRepository.AddAsync(user);
                return Ok(newUser);
            }
            catch
            {
                return StatusCode(500, "Internal servor error");
            }
        }
    }

}
