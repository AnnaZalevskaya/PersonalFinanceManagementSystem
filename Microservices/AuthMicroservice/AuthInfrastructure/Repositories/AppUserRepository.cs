using Auth.Application.Interfaces;
using Auth.Core.Entities;
using Auth.Infrastructure.Data;

namespace Auth.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository<AppUser>
    {
        private readonly AuthDbContext _context;

        public AppUserRepository(AuthDbContext context) 
        {
            _context = context;
        }

        public List<AppUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public AppUser GetById(long id)
        {
            var result = _context.Users.FirstOrDefault(user => user.Id == id);

            return result;
        }

        public AppUser FindByEmail(string email)
        {
            var result = _context.Users.FirstOrDefault(user => user.Email == email);

            return result;
        }

        public async Task<long> Add(AppUser entity)
        {
            var result = await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
