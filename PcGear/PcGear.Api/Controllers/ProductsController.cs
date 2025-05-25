using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsController(ProductsService productsService) : ControllerBase
    {
        [HttpPost("add_product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            await productsService.AddProductAsync(request);
            return Ok("Product added successfully");
        }

        [HttpGet("get_products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productsService.GetAllProductsAsync();
            return Ok(result);
        }


        [HttpGet("get_products_by_id:{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productsService.GetProductByIdAsync(id);
            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }


        [HttpGet("get_producs_with_reviews{id}")]
        public async Task<IActionResult> GetProductWithReviews(int id)
        {
            var result = await productsService.GetProductWithReviewsAsync(id);
            return Ok(result);
        }

        [HttpPut("update_product{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            await productsService.UpdateProductAsync(id, request);
            return Ok("Product updated successfully");
        }


        [HttpPatch("update_product_stock{id}")]
        public async Task<IActionResult> UpdateProductStock(int id, [FromBody] UpdateProductStockRequest request)
        {
            await productsService.UpdateProductStockAsync(id, request);
            return Ok("Product stock updated successfully");
        }


        [HttpDelete("delete_product{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await productsService.DeleteProductAsync(id);
            return Ok("Product deleted successfully");
        }



    }
}
