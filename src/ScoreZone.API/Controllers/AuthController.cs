using ScoreZone.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace ScoreZone.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        

        
        [HttpPost("login")]
        [EndpointSummary("Login")]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO request)
        {
            var result = await _service.LoginAsync(request);

            return HandleResult(result);
        }


        [HttpPost("register")]
        [EndpointSummary("Register")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterRequestDTO request)
        {
            var result = await _service.RegisterAsync(request);

            return HandleResult(result);
        }


        [HttpPut("reset-password")]
        [EndpointSummary("Reset Password")]
        [EndpointDescription("Reset Password Endpoint For Player, Employee & Owner")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            var result = await _service.ResetPasswordAsync(request);

            return HandleResult(result);
        }


        [HttpGet("renew-token")]
        [EndpointSummary("New Access Token")]
        [EndpointDescription("Create New Access Token Using Refresh Token")]
        [ProducesResponseType(typeof(TokenDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenewToken([FromQuery] string refreshToken)
        {
            var result = await _service.RenewToken(refreshToken);

            return HandleResult(result);
        }

    }
}