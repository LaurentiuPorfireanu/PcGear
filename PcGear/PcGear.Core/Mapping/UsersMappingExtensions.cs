using PcGear.Core.Dtos.BaseDtos.Users;
using PcGear.Core.Dtos.Requests;
using PcGear.Database.Entities;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace PcGear.Core.Mapping
{
    public static class UsersMappingExtensions
    {


        public static User ToEntity(this AddUserRequest request)
        {
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = "temp_password", 
                PasswordSalt = "temp_salt", 
                IsAdmin = request.IsAdmin,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin
            };
        }

        public static List<UserDto> ToUserDtos(this List<User> users)
        {
            return users.Select(u => u.ToUserDto()).ToList();
        }
    }
}
