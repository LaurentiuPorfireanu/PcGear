using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;
using System.Security.Claims;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login" });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration" });
            }
        }

        [HttpGet("profile(test)")]
        [Authorize]
        public IActionResult GetProfile()
        {
           
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            var firstNameClaim = User.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastNameClaim = User.FindFirst(ClaimTypes.Surname)?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Id = userIdClaim,
                Email = emailClaim,
                FirstName = firstNameClaim,
                LastName = lastNameClaim,
                Role = roleClaim,
                IsAdmin = roleClaim == "Admin"
            });
        }
    }
}
