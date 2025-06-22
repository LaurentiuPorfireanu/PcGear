using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;
using PcGear.Database.Dtos;


namespace PcGear.Api.Controllers
{

    [ApiController]
    [Route("api/products")]

    public class ProductsController(ProductsService productsService) : ControllerBase
    {
        
        [HttpPost("Add_product")]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            await productsService.AddProductAsync(request);
            return Ok("Product added successfully");
        }

        
        [HttpGet("Get_products")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productsService.GetAllProductsAsync();
            return Ok(result);
        }


        [HttpGet("Get_products_by_id:{id}")]
        [Authorize]

        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productsService.GetProductByIdAsync(id);
            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }


        [HttpGet("Get_producs_with_reviews{id}")]
        [Authorize]

        public async Task<IActionResult> GetProductWithReviews(int id)
        {
            var result = await productsService.GetProductWithReviewsAsync(id);
            return Ok(result);
        }

        [HttpPut("Update_product{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            await productsService.UpdateProductAsync(id, request);
            return Ok("Product updated successfully");
        }


        [HttpPatch("Update_product_stock{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateProductStock(int id, [FromBody] UpdateProductStockRequest request)
        {
            await productsService.UpdateProductStockAsync(id, request);
            return Ok("Product stock updated successfully");
        }


        [HttpDelete("Delete_product{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await productsService.DeleteProductAsync(id);
            return Ok("Product deleted successfully");
        }



        [HttpGet("Get_paged_and_filter")]
        [Authorize]
        public async Task<IActionResult> GetFilteredProductsPaged([FromQuery] ProductFilterRequest filter)
        {
            var result = await productsService.GetFilteredProductsPagedAsync(filter);
            return Ok(result.Data); 
        }



        

        
        
    }

}
