using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAppUserRepository<AppUser> Users { get; }
        IRoleRepository<IdentityRole> Roles { get; }
        IUserRoleRepository<IdentityUserRole<long>> UserRoles { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
