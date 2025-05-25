using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController(CategoriesService categoriesService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
        {
            await categoriesService.AddCategoryAsync(request);
            return Ok("Category added successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await categoriesService.GetAllCategoriesAsync();
            return Ok(result);
        }
    }
}
