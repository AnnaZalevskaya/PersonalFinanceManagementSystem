using MongoDB.Driver;
using Operations.Application.Interfaces;
using Operations.Application.Settings;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;

namespace Operations.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OperationsDbContext _context;

        public CategoryRepository(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .Find(categories => true)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return categories;
        }
    }
}
