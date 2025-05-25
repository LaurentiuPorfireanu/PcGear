using PcGear.Database.Context;
using PcGear.Database.Entities;
using PcGear.Database.Repos;

namespace PcGear.Database.Repositories;

public class UsersRepository(PcGearDatabaseContext databaseContext) : BaseRepository<User>(databaseContext)
{
    public async Task AddAsync(User user)
    {
        databaseContext.Users.Add(user);
        await SaveChangesAsync();
    }
}