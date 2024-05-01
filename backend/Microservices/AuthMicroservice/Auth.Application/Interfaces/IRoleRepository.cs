using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Interfaces
{
    public interface IRoleRepository<IdentityRole>
    {
        Task<List<IdentityRole<long>>> GetRoleIdsAsync(IEnumerable<long> roleIds, CancellationToken cancellationToken);
    }
}
