using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auth.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, List<IdentityRole<long>> role);
        string GetToken(AppUser user, List<IdentityRole<long>> role);
        List<Claim> GetClaimsFromExpiredAccessToken(string expiredAccessToken);
        string UpdateToken(List<Claim> claims);
        string GetRefreshToken(AppUser user);
    }
}
