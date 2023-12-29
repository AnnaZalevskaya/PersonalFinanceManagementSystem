using Auth.Application.Extensions;
using Auth.Application.Interfaces;
using Auth.Application.Settings;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtSettings> _options;

        public TokenService(IOptions<JwtSettings> options)
        {
            _options = options;
        }

        public string CreateToken(AppUser user, List<IdentityRole<long>> roles)
        {
            var token = user
                .CreateClaims(roles)
                .CreateJwtToken(_options);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public string GetToken(AppUser user, List<IdentityRole<long>> roles)
        {
            var accessToken = CreateToken(user, roles);
            user.RefreshToken = JwtExtention.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow
                .AddDays(int.Parse(_options.Value.RefreshTokenValidityInDays));

            return accessToken;
        }
    }
}
