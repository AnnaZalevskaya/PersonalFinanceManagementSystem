using Auth.Application.Models;
using Auth.Application.Settings;

namespace Auth.Application.Interfaces
{
    public interface IUsersService
    {
        Task<RegisterResponseModel> RegisterAsync(RegisterRequestModel request, CancellationToken cancellationToken);
        Task<AuthResponseModel> AuthenticateAsync(AuthRequestModel request, CancellationToken cancellationToken);
        Task<TokenModel> RefreshAccessToken(TokenModel tokens);
        Task<List<UserModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<int> GetRecordsCountAsync();
        Task<UserModel> GetUserByIdAsync(long id, CancellationToken cancellationToken);
    }
}
