using Microsoft.EntityFrameworkCore;
using PcGear.Database.Context;
using PcGear.Database.Entities;
using PcGear.Infrastructure.Exceptions;

namespace PcGear.Database.Repos
{
    public class UsersRepository(PcGearDatabaseContext databaseContext) : BaseRepository<User>(databaseContext)
    {
        public async Task AddAsync(User user)
        {
            databaseContext.Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var result = await databaseContext.Users
                .Where(e => e.Email == email)
                .Where(e => e.DeletedAt == null)
                .FirstOrDefaultAsync(cancellationTokenSource.Token);

            if (result == null)
                throw new ResourceMissingException("User not found");

            return result;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await databaseContext.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
        }

        public async Task UpdateAsync(User user)
        {
            user.ModifiedAt = DateTime.UtcNow;
            databaseContext.Users.Update(user);
            await SaveChangesAsync();
        }
    }
}