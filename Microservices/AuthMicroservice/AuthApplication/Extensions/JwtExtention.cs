using Auth.Application.Settings;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Extensions
{
    public static class JwtExtention
    {
        public static List<Claim> CreateClaims(this AppUser user, List<IdentityRole<long>> roles)
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

        public static SigningCredentials CreateSigningCredentials(this IOptions<JwtSettings> options)
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Value.Secret!)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }

        public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IOptions<JwtSettings> options)
        {
            var expire = int.Parse(options.Value.Expire);

            return new JwtSecurityToken(
                options.Value.Issuer,
                options.Value.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expire),
                signingCredentials: options.CreateSigningCredentials()
            );
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}