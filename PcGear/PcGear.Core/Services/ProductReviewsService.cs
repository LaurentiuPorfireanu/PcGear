using PcGear.Core.Dtos.BaseDtos.Products;
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

        public async Task<List<ProductReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await reviewsRepository.GetAllWithDetailsAsync();
            return reviews.Select(r => r.ToProductReviewDto()).ToList();
        }

        public async Task DeleteReviewAsync(int id)
        {
            await reviewsRepository.DeleteAsync(id);
        }
    }
}
