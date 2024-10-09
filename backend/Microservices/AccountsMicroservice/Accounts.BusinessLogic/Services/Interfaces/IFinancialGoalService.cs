using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Enums;
using Accounts.DataAccess.Settings;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IFinancialGoalService
    {
        Task<List<FinancialGoalModel>> GetFinancialGoalsAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken);
        Task<List<FinancialGoalModel>> GetAccountFinancialGoalsAsync(int accountId, PaginationSettings paginationSettings,
            CancellationToken cancellationToken);
        Task<int> GetAccountRecordsCountAsync(int accountId);
        Task<FinancialGoalModel> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateFinancialGoalAsync(FinancialGoalActionModel financialGoal, CancellationToken cancellationToken);
        Task UpdateAsync(int id, FinancialGoalActionModel updateModel, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task UpdateGoalStatusAsync(int goalId, GoalStatusEnum newStatus);
    }
}
