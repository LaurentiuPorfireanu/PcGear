using Microsoft.EntityFrameworkCore;
using PcGear.Database.Context;
using PcGear.Database.Entities;

namespace PcGear.Database.Repos
{
    public class ManufacturersRepository(PcGearDatabaseContext databaseContext) : BaseRepository<Manufacturer>(databaseContext)
    {
        public async Task AddAsync(Manufacturer manufacturer)
        {
            databaseContext.Manufacturers.Add(manufacturer);
            await SaveChangesAsync();
        }

        public async Task<Manufacturer?> GetByIdAsync(int id)
        {
            return await databaseContext.Manufacturers
                .Where(m => m.Id == id && m.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Manufacturer manufacturer)
        {
            manufacturer.ModifiedAt = DateTime.UtcNow;
            databaseContext.Manufacturers.Update(manufacturer);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var manufacturer = await GetByIdAsync(id);
            if (manufacturer != null)
            {
                manufacturer.DeletedAt = DateTime.UtcNow;
                await UpdateAsync(manufacturer);
            }
        }
    }
}