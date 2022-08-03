using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapitest.Contracts.V1.Requests;
using webapitest.Services;

namespace webapitest.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] UserRegisterDto request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);
            if (authResponse == String.Empty)
            {
                return BadRequest();
            }
            return new OkObjectResult(new {Token = authResponse});
        }  
        [HttpGet("Login")]
        public async Task<object> Login([FromQuery] UserRegisterDto request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (authResponse == String.Empty)
            {
                return BadRequest();
            }
            return new OkObjectResult(new {Token = authResponse});
        }
    }
}