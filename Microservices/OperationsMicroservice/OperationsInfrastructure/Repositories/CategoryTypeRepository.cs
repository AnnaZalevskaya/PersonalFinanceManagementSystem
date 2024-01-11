using MongoDB.Driver;
using Operations.Application.Interfaces;
using Operations.Application.Settings;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;

namespace Operations.Infrastructure.Repositories
{
    public class CategoryTypeRepository : ICategoryTypeRepository
    {
        private readonly OperationsDbContext _context;

        public CategoryTypeRepository(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryType> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.CategoryTypes
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<CategoryType>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var types = await _context.CategoryTypes
                .Find(types => true)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return types;
        }
    }
}
