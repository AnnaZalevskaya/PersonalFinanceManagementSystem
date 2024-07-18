using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Settings;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IFinancialAccountService
    {
        Task AddAsync(FinancialAccountActionModel addModel, CancellationToken cancellationToken);
        Task DeleteAsync(int userId, int id, CancellationToken cancellationToken);
        Task<List<FinancialAccountModel>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken);
        Task<List<FinancialAccountModel>> GetAccountsByUserIdAsync(int userId, PaginationSettings paginationSettings, 
            CancellationToken cancellationToken);
        Task<FinancialAccountModel> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(int userId, int id, FinancialAccountActionModel updateModel, CancellationToken cancellationToken);
        Task<int> GetRecordsCountAsync();
        Task<int> GetUserRecordsCountAsync(int userId);
    }
}
