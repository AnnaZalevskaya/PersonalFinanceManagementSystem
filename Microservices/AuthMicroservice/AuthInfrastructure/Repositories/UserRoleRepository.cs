using Auth.Application.Interfaces;
using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository<IdentityUserRole<long>>
    {
        private readonly AuthDbContext _context;

        public UserRoleRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<long>> GetRoleIdsAsync(AppUser user)
        {
            return await _context.UserRoles
                .Where(r => r.UserId == user.Id)
                .Select(x => x.RoleId)
                .ToListAsync();
        }
    }
}
