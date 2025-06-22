using Microsoft.EntityFrameworkCore;
using PcGear.Database.Context;
using PcGear.Database.Entities;

namespace PcGear.Database.Repos
{
    public class CategoriesRepository(PcGearDatabaseContext databaseContext) : BaseRepository<Category>(databaseContext)
    {
        public async Task AddAsync(Category category)
        {
            databaseContext.Categories.Add(category);
            await SaveChangesAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await databaseContext.Categories
                .Where(c => c.Id == id && c.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            category.ModifiedAt = DateTime.UtcNow;
            databaseContext.Categories.Update(category);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                category.DeletedAt = DateTime.UtcNow;
                await UpdateAsync(category);
            }
        }
    }
}