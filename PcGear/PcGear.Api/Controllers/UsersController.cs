using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(UsersService usersService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            await usersService.AddUserAsync(request);
            return Ok("User added successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await usersService.GetAllUsersAsync();
            return Ok(result);
        }
    }
}
