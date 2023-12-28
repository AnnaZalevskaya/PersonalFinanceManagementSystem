using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Interfaces
{
    public interface IRoleRepository<IdentityRole>
    {
        List<IdentityRole<long>> GetRoleIds(IEnumerable<long> roleIds);
    }
}
