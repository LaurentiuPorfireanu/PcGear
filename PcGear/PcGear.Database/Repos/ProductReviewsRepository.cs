using PcGear.Database.Context;
using PcGear.Database.Entities;
using PcGear.Database.Repos;

namespace PcGear.Database.Repositories;

public class ProductReviewsRepository(PcGearDatabaseContext databaseContext) : BaseRepository<ProductReview>(databaseContext)
{
    public async Task AddAsync(ProductReview review)
    {
        databaseContext.ProductReviews.Add(review);
        await SaveChangesAsync();
    }
}