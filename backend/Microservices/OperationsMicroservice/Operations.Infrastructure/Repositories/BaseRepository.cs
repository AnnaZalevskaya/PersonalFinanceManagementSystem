using MongoDB.Driver;
using Operations.Application.Interfaces;
using Operations.Application.Settings;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;
using System.Text.RegularExpressions;

namespace Operations.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly OperationsDbContext _context;

        public BaseRepository(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var collectionName = Regex.Replace(typeof(TEntity).Name, "(?<=.)([A-Z])", "_$1", 
                RegexOptions.Compiled).TrimStart('_').ToLower();
            var collection = _context.Database.GetCollection<TEntity>(collectionName);

            return await collection
                .Find(entity => true)
                .SortBy(entity => entity.Id)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            var collectionName = Regex.Replace(typeof(TEntity).Name, "(?<=.)([A-Z])", "_$1",
                RegexOptions.Compiled).TrimStart('_').ToLower();
            var collection = _context.Database.GetCollection<TEntity>(collectionName);

            return await collection
                .Find(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
