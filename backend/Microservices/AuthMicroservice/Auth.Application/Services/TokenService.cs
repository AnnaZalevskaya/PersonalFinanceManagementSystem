using Auth.Application.Exceptions;
using Auth.Application.Extensions;
using Auth.Application.Interfaces;
using Auth.Application.Settings;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        public string UpdateToken(List<Claim> claims)
        {
            var token = claims
                .CreateJwtToken(_options);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public string GetToken(AppUser user, List<IdentityRole<long>> roles)
        {
            var accessToken = CreateToken(user, roles);

            if (accessToken.ValidateToken(_options))
            {
                user.RefreshToken = GetRefreshToken(user);

                return accessToken;
            }
            else
            {
                throw new NotValidTokenException();
            }
        }

        public string GetRefreshToken(AppUser user)
        {
            if (user.RefreshToken == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                user.RefreshToken = JwtExtention.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow
                    .AddDays(_options.Value.RefreshTokenValidityInDays);
            }

            return user.RefreshToken;
        }

        public List<Claim> GetClaimsFromExpiredAccessToken(string expiredAccessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(expiredAccessToken) as JwtSecurityToken;

            if (securityToken == null)
            {
                throw new SecurityTokenException("Invalid access token");
            }

            return securityToken.Claims.ToList();
        }
    }
}
