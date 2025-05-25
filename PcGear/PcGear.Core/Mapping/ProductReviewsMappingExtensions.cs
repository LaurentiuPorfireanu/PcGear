using PcGear.Core.Dtos.Requests;
using PcGear.Database.Entities;

namespace PcGear.Core.Mapping
{
    public static class ProductReviewsMappingExtensions
    {
        public static ProductReview ToEntity(this AddProductReviewRequest request)
        {
            return new ProductReview
            {
                ProductId = request.ProductId,
                UserId = request.UserId,
                Rating = request.Rating,
                ReviewText = request.ReviewText,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
