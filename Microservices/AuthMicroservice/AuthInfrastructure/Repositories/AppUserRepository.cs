using Auth.Application.Interfaces;
using Auth.Application.Settings;
using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository<AppUser>
    {
        private readonly AuthDbContext _context;

        public AppUserRepository(AuthDbContext context) 
        {
            _context = context;
        }

        public async Task<List<AppUser>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<AppUser> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

            return result;
        }

        public async Task<AppUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

            return result;
        }

        public async Task<long> AddAsync(AppUser entity, CancellationToken cancellationToken)
        {
            var result = await _context.Users.AddAsync(entity, cancellationToken);

            return result.Entity.Id;
        }
    }
}
