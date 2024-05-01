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

        public async Task<FinancialAccount> FindAccountByNameAsync(string name, CancellationToken cancellationToken)
        {
            var account = await _context.FinancialAccounts
                .Where(account => account.Name == name)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return account;
        }

        public async Task<IEnumerable<FinancialAccount>> GetFullAccounts(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var accounts = await _context.FinancialAccounts
                .Include(account => account.AccountType)
                .Include(account => account.Currency)
                .OrderBy(e => e.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return accounts;
        }

        public async Task<IEnumerable<FinancialAccount>> GetAccountsByUserIdAsync(int userId, 
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var userAccounts = await _context.FinancialAccounts
                .Include(account => account.AccountType)
                .Include(account => account.Currency)
                .Where(account => account.UserId == userId)
                .OrderBy(e => e.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return userAccounts;
        }

        public async Task<FinancialAccount> GetFullAccountByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.FinancialAccounts
                .Include(account => account.AccountType)
                .Include(account => account.Currency)
                .FirstOrDefaultAsync(account => account.Id == id, cancellationToken);
        }
    }
}
