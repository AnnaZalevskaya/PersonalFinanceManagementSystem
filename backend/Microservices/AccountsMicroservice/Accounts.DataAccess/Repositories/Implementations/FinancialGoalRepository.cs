using Accounts.DataAccess.Data;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.EntityFrameworkCore;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class FinancialGoalRepository : IFinancialGoalRepository
    {
        private readonly FinancialAccountsDbContext _context;

        public FinancialGoalRepository(FinancialAccountsDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FinancialGoal goal, CancellationToken cancellationToken)
        {
            await _context.AddAsync(goal, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entityToDelete = await _context.FinancialGoals
                .Where(goal => goal.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            _context.FinancialGoals.Remove(entityToDelete);
        }

        public async Task<IEnumerable<FinancialGoal>> GetAllAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            return await _context.FinancialGoals
                .Include(goal => goal.Type)
                .OrderBy(e => e.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<FinancialGoal> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.FinancialGoals
                .Include(goal => goal.Type)
                .Where(goal => goal.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, FinancialGoal item, CancellationToken cancellationToken)
        {
            var entityToUpdate = await _context.FinancialGoals
                .Where(goal => goal.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            _context.FinancialGoals.Entry(entityToUpdate).CurrentValues.SetValues(item);
        }

        public async Task<int> GetRecordsCountAsync()
        {
            return await _context.FinancialGoals.CountAsync();
        }

        public async Task<IEnumerable<FinancialGoal>> GetAccountGoalsAsync(int accountId,
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var accountGoals = await _context.FinancialGoals
                .Include(account => account.Type)
                .Where(account => account.AccountId == accountId)
                .OrderBy(e => e.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return accountGoals;
        }

        public async Task<int> GetAccountRecordsCountAsync(int accountId)
        {
            return await _context.FinancialGoals
                .Where(account => account.AccountId == accountId)
                .CountAsync();
        }
    }
}
