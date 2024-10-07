using Accounts.DataAccess.Dapper.Entities;

namespace Accounts.DataAccess.Dapper.Repositories.Interfaces
{
    public interface IAccountStatisticsRepository
    {
        Task<List<AccountStatistics>> GetStatisticByAccountsAsync(int accountTypeParam);
    }
}
