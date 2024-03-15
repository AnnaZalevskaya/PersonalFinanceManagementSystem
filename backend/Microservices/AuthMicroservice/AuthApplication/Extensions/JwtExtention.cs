using Auth.Application.Settings;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Auth.Application.Extensions
{
    public static class JwtExtention
    {
        public static List<Claim> CreateClaims(this AppUser user, List<IdentityRole<long>> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
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
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Secret!)),
                SecurityAlgorithms.HmacSha256
            );
        }

        public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IOptions<JwtSettings> options)
        {
            var expire = int.Parse(options.Value.Expire);
            var jwtToken = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expire),
                signingCredentials: options.CreateSigningCredentials()
            );

            return jwtToken;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public static bool ValidateToken(this string token, IOptions<JwtSettings> options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Secret));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = options.Value.Issuer,
                ValidAudience = options.Value.Audience
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out _);

                return true; 
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}