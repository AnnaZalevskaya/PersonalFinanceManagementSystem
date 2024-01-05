using Accounts.DataAccess.Data;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.EntityFrameworkCore;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class FinancialAccountRepository : BaseRepository<FinancialAccount>, IFinancialAccountRepository
    {
        public FinancialAccountRepository(FinancialAccountsDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FinancialAccount>> GetAccountsByUserIdAsync(int userId, 
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var userAccounts = await _context.FinancialAccounts
                .Where(account => account.UserId == userId)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return userAccounts;
        }
    }
}
