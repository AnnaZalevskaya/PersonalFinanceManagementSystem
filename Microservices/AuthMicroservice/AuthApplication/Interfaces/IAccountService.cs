using Auth.Application.Models;
using Auth.Application.Settings;

namespace Auth.Application.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<AuthResponse> AuthenticateAsync(AuthRequest request, CancellationToken cancellationToken);
        Task<List<UserModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}
