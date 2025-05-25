using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/manufacturers")]
    public class ManufacturersController(ManufacturersService manufacturersService) : ControllerBase
    {
        [HttpPost("add manufacturer")]
        public async Task<IActionResult> AddManufacturer([FromBody] AddManufacturerRequest request)
        {
            await manufacturersService.AddManufacturerAsync(request);
            return Ok("Manufacturer added successfully");
        }

        [HttpGet("get_manufacturers")]
        public async Task<IActionResult> GetAllManufacturers()
        {
            var result = await manufacturersService.GetAllManufacturersAsync();
            return Ok(result);
        }
    }
}
