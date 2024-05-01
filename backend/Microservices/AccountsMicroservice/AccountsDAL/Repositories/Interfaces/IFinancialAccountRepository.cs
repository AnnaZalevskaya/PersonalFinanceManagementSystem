using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Settings;

namespace Accounts.DataAccess.Repositories.Interfaces
{
    public interface IFinancialAccountRepository : IBaseRepository<FinancialAccount>
    {
        Task<FinancialAccount> FindAccountByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<FinancialAccount>> GetFullAccounts(PaginationSettings paginationSettings,
            CancellationToken cancellationToken);
        Task<IEnumerable<FinancialAccount>> GetAccountsByUserIdAsync(int userId, 
            PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<FinancialAccount> GetFullAccountByIdAsync(int id, CancellationToken cancellationToken);
    }
}
