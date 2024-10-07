using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IAccountStatisticsService
    {
        Task<List<AccountStatisticsModel>> GetStatisticByAccountsAsync(int accountTypeParam);
    }
}
