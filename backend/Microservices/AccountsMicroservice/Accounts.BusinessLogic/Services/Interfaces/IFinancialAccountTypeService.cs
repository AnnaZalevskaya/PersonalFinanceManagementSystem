using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Settings;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IFinancialAccountTypeService
    {
        Task<List<FinancialAccountTypeModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<int> GetRecordsCountAsync();
        Task<FinancialAccountTypeModel> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
