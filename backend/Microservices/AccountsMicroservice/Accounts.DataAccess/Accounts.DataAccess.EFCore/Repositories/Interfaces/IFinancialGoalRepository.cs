using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Settings;

namespace Accounts.DataAccess.Repositories.Interfaces
{
    public interface IFinancialGoalRepository : IBaseRepository<FinancialGoal>
    {
        Task<IEnumerable<FinancialGoal>> GetAccountGoalsAsync(int accountId,
            PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<int> GetAccountRecordsCountAsync(int accountId);
    }
}
