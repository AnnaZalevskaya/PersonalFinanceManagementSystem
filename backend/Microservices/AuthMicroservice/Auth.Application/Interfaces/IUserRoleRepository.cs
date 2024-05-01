using Auth.Core.Entities;

namespace Auth.Application.Interfaces
{
    public interface IUserRoleRepository<IdentityUserRole>
    {
        Task<IEnumerable<long>> GetRoleIdsAsync(AppUser user, CancellationToken cancellationToken);
    }
}
