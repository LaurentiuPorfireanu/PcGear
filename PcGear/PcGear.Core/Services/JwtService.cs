using Microsoft.IdentityModel.Tokens;
using PcGear.Core.Dtos.Responses;
using PcGear.Database.Entities;
using PcGear.Infrastructure.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PcGear.Core.Services
{
    public class JwtService
    {
        public string GenerateToken(User user)
        {
            var jwtSettings = AppConfig.JWTSettings;
            if (jwtSettings == null)
                throw new InvalidOperationException("JWT settings not configured");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecurityKey);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.GivenName, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
                new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryInMinutes),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthResponse CreateAuthResponse(User user, string token)
        {
            var jwtSettings = AppConfig.JWTSettings;

            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                ExpiresAt = DateTime.UtcNow.AddMinutes(jwtSettings?.ExpiryInMinutes ?? 60)
            };
        }
    }
}