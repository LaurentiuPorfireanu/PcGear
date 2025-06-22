using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/manufacturers")]

    public class ManufacturersController(ManufacturersService manufacturersService) : ControllerBase
    {
        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddManufacturer([FromBody] AddManufacturerRequest request)
        {
            await manufacturersService.AddManufacturerAsync(request);
            return Ok(new { message = "Manufacturer added successfully" });
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> GetAllManufacturers()
        {
            var result = await manufacturersService.GetAllManufacturersAsync();
            return Ok(result);
        }

        [HttpGet("Get_by_id{id}")]
        [Authorize]
        public async Task<IActionResult> GetManufacturerById(int id)
        {
            var result = await manufacturersService.GetManufacturerByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Manufacturer not found" });

            return Ok(result);
        }

        [HttpPatch("Patch_by_id{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateManufacturer(int id, [FromBody] UpdateManufacturerRequest request)
        {
            await manufacturersService.UpdateManufacturerAsync(id, request);
            return Ok(new { message = "Manufacturer updated successfully" });
        }

        [HttpDelete("Delete_by_id{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            await manufacturersService.DeleteManufacturerAsync(id);
            return Ok(new { message = "Manufacturer deleted successfully" });
        }
    }
}