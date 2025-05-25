using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(UsersService usersService) : ControllerBase
    {
        [HttpPost("add_user")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            await usersService.AddUserAsync(request);
            return Ok("User added successfully");
        }

        [HttpGet("get all users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await usersService.GetAllUsersAsync();
            return Ok(result);
        }
    }
}
