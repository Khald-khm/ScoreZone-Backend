using ScoreZone.Application.Auth;
using ScoreZone.Application.Shared.Results;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ScoreZone.API.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO request)
        {
            var result = await _service.LoginAsync(request);

            return HandleResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDTO request)
        {
            var result = await _service.RegisterAsync(request);

            return HandleResult(result);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            var result = await _service.ResetPasswordAsync(request);

            return HandleResult(result);
        }
    }
}