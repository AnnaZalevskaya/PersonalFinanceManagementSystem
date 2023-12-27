using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.API.Extensions
{
    public static class JwtExtention
    {
        public static List<Claim> CreateClaims(this AppUser user, List<IdentityRole> roles)
        {
            var claims = new List<Claim>
            {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.MobilePhone, user.PhoneNumber!),
            new(ClaimTypes.Role, string.Join(" ", roles.Select(x => x.Name))),
            };

            return claims;
        }

        public static SigningCredentials CreateSigningCredentials(this IConfiguration configuration)
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }

        public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IConfiguration configuration)
        {
            var expire = int.Parse(configuration.GetSection("Jwt:Expire").Value);

            return new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expire),
                signingCredentials: configuration.CreateSigningCredentials()
            );
        }

        public static string GenerateRefreshToken(this IConfiguration configuration)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}