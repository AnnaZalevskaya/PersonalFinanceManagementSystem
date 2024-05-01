using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Settings;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<CurrencyModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<CurrencyModel> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
