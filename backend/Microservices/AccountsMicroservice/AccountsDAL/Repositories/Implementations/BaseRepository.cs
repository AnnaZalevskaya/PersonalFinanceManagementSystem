using Accounts.DataAccess.Data;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Microsoft.EntityFrameworkCore;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly FinancialAccountsDbContext _context;

        public BaseRepository(FinancialAccountsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity item, CancellationToken cancellationToken)
        {
            await _context.AddAsync(item, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entityToDelete = await _context.FindAsync<TEntity>(id, cancellationToken);
            _context.Remove(entityToDelete);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>()
                .OrderBy(e => e.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.FindAsync<TEntity>(id, cancellationToken);
        }

        public async Task UpdateAsync(int id, TEntity item, CancellationToken cancellationToken)
        {
            var entityToUpdate = await _context.FindAsync<TEntity>(id, cancellationToken);
            _context.Entry<TEntity>(entityToUpdate).CurrentValues.SetValues(item);
        }
    }
}
