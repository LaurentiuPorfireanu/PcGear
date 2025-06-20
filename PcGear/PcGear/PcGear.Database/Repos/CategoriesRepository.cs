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
    }
}
