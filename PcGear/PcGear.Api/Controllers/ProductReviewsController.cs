using Microsoft.AspNetCore.Mvc;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Services;

namespace PcGear.Api.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ProductReviewsController(ProductReviewsService reviewsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddProductReviewRequest request)
        {
            await reviewsService.AddReviewAsync(request);
            return Ok("Review added successfully");
        }
    }
}
