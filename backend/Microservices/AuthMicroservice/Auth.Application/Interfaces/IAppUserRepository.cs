using Auth.Application.Settings;

namespace Auth.Application.Interfaces
{
    public interface IAppUserRepository<AppUser>
    {
        Task<List<AppUser>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<int> GetRecordsCountAsync();
        Task<AppUser> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<AppUser> FindByEmailAsync(string email, CancellationToken cancellationToken);
        Task<long> AddAsync(AppUser entity, CancellationToken cancellationToken);
    }
}
