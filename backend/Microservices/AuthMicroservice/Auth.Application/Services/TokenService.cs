using Auth.Application.Exceptions;
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

            if (accessToken.ValidateToken(_options))
            {
                RefreshToken(user);

                return accessToken;
            }
            else
            {
                throw new NotValidTokenException();
            }
        }

        private void RefreshToken(AppUser user)
        {
            if (user.RefreshToken == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                user.RefreshToken = JwtExtention.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow
                    .AddDays(_options.Value.RefreshTokenValidityInDays);
            }
        }
    }
}
