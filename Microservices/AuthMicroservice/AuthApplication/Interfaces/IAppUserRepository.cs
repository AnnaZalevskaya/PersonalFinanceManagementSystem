namespace Auth.Application.Interfaces
{
    public interface IAppUserRepository<AppUser>
    {
        Task<List<AppUser>> GetAllAsync(CancellationToken cancellationToken);
        Task<AppUser> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<AppUser> FindByEmailAsync(string email, CancellationToken cancellationToken);
        Task<long> AddAsync(AppUser entity, CancellationToken cancellationToken);
        Task SaveChangesAsync();
    }
}
