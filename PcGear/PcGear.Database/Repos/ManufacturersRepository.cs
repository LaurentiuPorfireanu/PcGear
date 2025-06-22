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

        
    }
}