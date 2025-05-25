using PcGear.Core.Dtos.Requests;
using PcGear.Core.Mapping;
using PcGear.Database.Repos;

namespace PcGear.Core.Services
{
    public class ProductReviewsService(ProductReviewsRepository reviewsRepository)
    {
        public async Task AddReviewAsync(AddProductReviewRequest request)
        {
            var review = request.ToEntity();
            await reviewsRepository.AddAsync(review);
        }
    }
}
