using Accounts.DataAccess.Dapper.Data;
using Accounts.DataAccess.Dapper.Entities;
using Accounts.DataAccess.Dapper.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace Accounts.DataAccess.Dapper.Repositories.Implementations
{
    public class AccountStatisticsRepository : IAccountStatisticsRepository
    {
        private readonly DapperContext _context;

        public AccountStatisticsRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<AccountStatistics>> GetStatisticByAccountsAsync(int accountTypeParam)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new { account_type_param = accountTypeParam };

                var result = await connection.QueryAsync<AccountStatistics>(
                    "select * from accounts.get_account_statistic(@account_type_param)", 
                    parameters);

                return result.AsList();
            }
        }
    }
}
