using Auth.Application.Models;
using Auth.Core.Entities;

namespace Auth.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponse> Register(RegisterRequest request);
        Task<AuthResponse> Authenticate(AuthRequest request);
        List<AppUser> GetAll();

    }
}
