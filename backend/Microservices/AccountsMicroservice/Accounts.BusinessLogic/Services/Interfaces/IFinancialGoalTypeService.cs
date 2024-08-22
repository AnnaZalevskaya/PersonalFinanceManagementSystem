using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Settings;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IFinancialGoalTypeService
    {
        Task<List<FinancialGoalTypeModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<int> GetRecordsCountAsync();
        Task<FinancialGoalTypeModel> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
