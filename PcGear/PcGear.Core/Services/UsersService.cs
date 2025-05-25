using PcGear.Core.Dtos.BaseDtos.Users;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Mapping;
using PcGear.Database.Repositories;

namespace PcGear.Core.Services
{
    public class UsersService(UsersRepository usersRepository)
    {
        public async Task AddUserAsync(AddUserRequest request)
        {
            var user = request.ToEntity();
            await usersRepository.AddAsync(user);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await usersRepository.GetAllAsync();
            return users.ToUserDtos();
        }
    }
}
