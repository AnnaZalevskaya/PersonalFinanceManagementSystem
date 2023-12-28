using Auth.Application.Interfaces;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository<IdentityRole>
    {
        private readonly AuthDbContext _context;

        public RoleRepository(AuthDbContext context)
        {
            _context = context;
        }

        public List<IdentityRole<long>> GetRoleIds(IEnumerable<long> roleIds)
        {
            return _context.Roles
                .Where(x => roleIds.Contains(x.Id))
                .ToList();
        }
    }
}
