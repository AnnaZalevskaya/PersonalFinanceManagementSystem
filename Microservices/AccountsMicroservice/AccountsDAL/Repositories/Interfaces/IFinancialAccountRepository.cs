using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Settings;

namespace Accounts.DataAccess.Repositories.Interfaces
{
    public interface IFinancialAccountRepository : IBaseRepository<FinancialAccount>
    {
        Task<IEnumerable<FinancialAccount>> GetAccountsByUserIdAsync(int userId, 
            PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}
