namespace Auth.Application.Interfaces
{
    public interface IAppUserRepository<AppUser>
    {
        List<AppUser> GetAll();
        AppUser GetById(long id);
        AppUser FindByEmail(string email);
        Task<long> AddAsync(AppUser entity);
        Task SaveChangesAsync();
    }
}
