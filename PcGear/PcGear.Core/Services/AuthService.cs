using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PcGear.Database.Entities;
using PcGear.Infrastructure.Config;
using PcGear.Core.Dtos.Requests;
using PcGear.Database.Repos;
using PcGear.Infrastructure.Exceptions;

namespace PcGear.Core.Services
{
    public class AuthService
    {
        private readonly UsersRepository _usersRepository;

        public AuthService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<object> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _usersRepository.GetByEmailAsync(request.Email);

                if (HashPassword(request.Password, Convert.FromBase64String(user.PasswordSalt)) == user.Password)
                {
                    var role = GetRole(user);
                    var token = GetToken(user, role);

                    return new
                    {
                        token = token,
                        user = new
                        {
                            id = user.Id,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            email = user.Email,
                            isAdmin = user.IsAdmin
                        }
                    };
                }
                else
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }
            }
            catch (ResourceMissingException)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
        }



        public async Task<object> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Registration data is required");
            }

            
            if (request.Password != request.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match");
            }

      
            try
            {
                await _usersRepository.GetByEmailAsync(request.Email);
                throw new ArgumentException("Email already exists");
            }
            catch (ResourceMissingException)
            {
             
            }

            var salt = GenerateSalt();
            var hashedPassword = HashPassword(request.Password, salt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = hashedPassword,
                PasswordSalt = Convert.ToBase64String(salt),
                IsAdmin = request.IsAdmin,
                CreatedAt = DateTime.UtcNow
            };

            await _usersRepository.AddAsync(user);

            return new { message = "Registration successful", userId = user.Id };
        }

        public string GetToken(User user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JWTSettings.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.GivenName, user.FirstName),
        new Claim(ClaimTypes.Surname, user.LastName),
        new Claim(ClaimTypes.Role, role),

        new Claim("userId", user.Id.ToString()),
        new Claim("email", user.Email),
        new Claim("role", role)
    };

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;
        }
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public string HashPassword(string password, byte[] salt)
        {
            var bytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(bytes);
        }

        private string GetRole(User user)
        {
            return user.IsAdmin ? "Admin" : "User";
        }
    }
}