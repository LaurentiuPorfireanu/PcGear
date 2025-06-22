using Microsoft.EntityFrameworkCore;
using PcGear.Database.Context;
using PcGear.Database.Entities;
using PcGear.Database.Repos;

namespace PcGear.Database.Repos;

public class ProductReviewsRepository(PcGearDatabaseContext databaseContext) : BaseRepository<ProductReview>(databaseContext)
{
    public async Task AddAsync(ProductReview review)
    {
        databaseContext.ProductReviews.Add(review);
        await SaveChangesAsync();
    }

    public async Task<List<ProductReview>> GetAllWithDetailsAsync()
    {
        return await databaseContext.ProductReviews
            .Include(r => r.User)
            .Include(r => r.Product)
            .Where(r => r.DeletedAt == null)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
    public async Task<ProductReview?> GetByIdAsync(int id)
    {
        return await databaseContext.ProductReviews
            .Where(r => r.Id == id && r.DeletedAt == null)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(ProductReview review)
    {
        review.ModifiedAt = DateTime.UtcNow;
        databaseContext.ProductReviews.Update(review);
        await SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var review = await GetByIdAsync(id);
        if (review != null)
        {
            review.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(review);
        }
    }
}