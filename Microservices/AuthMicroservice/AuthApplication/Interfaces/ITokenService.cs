using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, List<IdentityRole<long>> role);
    }
}
