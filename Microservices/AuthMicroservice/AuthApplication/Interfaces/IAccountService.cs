using Auth.Application.Models;

namespace Auth.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<AuthResponse> AuthenticateAsync(AuthRequest request, CancellationToken cancellationToken);
        Task<List<UserModel>> GetAllAsync(CancellationToken cancellationToken);
    }
}
