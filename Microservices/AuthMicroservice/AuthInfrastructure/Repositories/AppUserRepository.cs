using Auth.Application.Interfaces;
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

        public async Task<List<AppUser>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
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
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
