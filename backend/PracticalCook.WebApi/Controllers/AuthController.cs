using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Auth;
using PracticalCook.Application.Dtos.User;
using PracticalCook.Application.Services.Auth;

namespace PracticalCook.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<Response<GetUserDto>>> Register([FromBody] UserDto request)
        {
            var response = await authService.RegisterAsync(request);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response<TokenResponseDto>>> Login([FromBody] UserDto request)
        {

            var response = await authService.LoginAsync(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<Response<TokenResponseDto>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var response = await authService.RefreshTokenAsync(request);
            if (!response.Success)
            {
                return Unauthorized(response.Message);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("auth-only")]
        public IActionResult AuthenticateOnlyEndpoint()
        {
            // This endpoint is only accessible to authenticated users
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            // This endpoint is only accessible to authenticated users
            return Ok("You are an admin!");
        }
    }
}
