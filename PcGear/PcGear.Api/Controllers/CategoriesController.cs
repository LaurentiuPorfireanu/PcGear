using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    
    public class CategoriesController(CategoriesService categoriesService) : ControllerBase
    {
        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
        {
            await categoriesService.AddCategoryAsync(request);
            return Ok(new { message = "Category added successfully" });
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await categoriesService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("Get_by_id{id}")]
        [Authorize]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await categoriesService.GetCategoryByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Category not found" });

            return Ok(result);
        }

        [HttpPut("Put_by_id{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest request)
        {
            await categoriesService.UpdateCategoryAsync(id, request);
            return Ok(new { message = "Category updated successfully" });
        }

        [HttpDelete("Delete_by_id{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoriesService.DeleteCategoryAsync(id);
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}