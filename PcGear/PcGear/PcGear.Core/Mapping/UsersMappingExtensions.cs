using PcGear.Core.Dtos.BaseDtos.Users;
using PcGear.Core.Dtos.Requests;
using PcGear.Database.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PcGear.Core.Mapping
{
    public static class UsersMappingExtensions
    {
        public static User ToEntity(this AddUserRequest request)
        {
            var (hashedPassword, salt) = HashPassword(request.Password);

            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.ToLower(),
                PhoneNumber = request.PhoneNumber,
                Password = hashedPassword,
                PasswordSalt = salt,
                IsAdmin = request.IsAdmin,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static User ToEntity(this RegisterRequest request)
        {
            var (hashedPassword, salt) = HashPassword(request.Password);

            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.ToLower(),
                PhoneNumber = request.PhoneNumber,
                Password = hashedPassword,
                PasswordSalt = salt,
                IsAdmin = false, // Default non-admin pentru înregistrare
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

        // Metodă pentru hashing password
        private static (string hashedPassword, string salt) HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[32];
            rng.GetBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var hashedPassword = HashPasswordWithSalt(password, salt);
            return (hashedPassword, salt);
        }

        // Metodă pentru verificarea parolei
        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            var hashToVerify = HashPasswordWithSalt(password, salt);
            return hashToVerify == hashedPassword;
        }

        private static string HashPasswordWithSalt(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashedBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}