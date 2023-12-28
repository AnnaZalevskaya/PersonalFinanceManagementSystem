using Auth.Application.Interfaces;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository<IdentityRole>
    {
        private readonly AuthDbContext _context;

        public RoleRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<IdentityRole<long>>> GetRoleIdsAsync(IEnumerable<long> roleIds)
        {
            return await _context.Roles
                .Where(x => roleIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}
