using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ProductReviewsController(ProductReviewsService reviewsService) : ControllerBase
    {
        [HttpPost("Add_review")]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] AddProductReviewRequest request)
        {
            await reviewsService.AddReviewAsync(request);
            return Ok("Review added successfully");
        }

        [HttpGet("Get_Reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await reviewsService.GetAllReviewsAsync();
            return Ok(result);
        }


        [HttpDelete("Delete_by_id{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await reviewsService.DeleteReviewAsync(id);
            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
