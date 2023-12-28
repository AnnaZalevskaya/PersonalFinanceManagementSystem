using Auth.Core.Entities;

namespace Auth.Application.Interfaces
{
    public interface IUserRoleRepository<IdentityUserRole>
    {
        Task<IEnumerable<long>> GetRoleIds(AppUser user);
    }
}
