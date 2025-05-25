using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsController(ProductsService productsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            await productsService.AddProductAsync(request);
            return Ok("Product added successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productsService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("{id}/with-reviews")]
        public async Task<IActionResult> GetProductWithReviews(int id)
        {
            var result = await productsService.GetProductWithReviewsAsync(id);
            return Ok(result);
        }
    }
}
