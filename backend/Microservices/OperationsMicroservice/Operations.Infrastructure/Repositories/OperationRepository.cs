using MongoDB.Driver;
using Operations.Application.Interfaces;
using Operations.Application.Settings;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;

namespace Operations.Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly OperationsDbContext _context;

        public OperationRepository(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Operation operation, CancellationToken cancellationToken)
        {
            await _context.Operations.InsertOneAsync(operation, cancellationToken);
        }

        public async Task DeleteByAccountIdAsync(int accountId, CancellationToken cancellationToken)
        {
            await _context.Operations.DeleteManyAsync(c => c.AccountId == accountId, cancellationToken);
        }

        public async Task<Operation> GetAsync(string id, CancellationToken cancellationToken)
        {
            var operation = await _context.Operations
                .Find(operation => operation.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            return operation;
        }

        public async Task<IEnumerable<Operation>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var operations = await _context.Operations
                .Find(operations => true)
                .SortBy(operation => operation.AccountId)
                .ThenBy(operation => operation.Date)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return operations;
        }

        public async Task<IEnumerable<Operation>> GetByAccountIdAsync(int accountId,
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var operations = await _context.Operations
                .Find(operation => operation.AccountId == accountId)
                .SortBy(operation => operation.Date)
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Limit(paginationSettings.PageSize)
                .ToListAsync(cancellationToken);

            return operations;
        }
    }
}
