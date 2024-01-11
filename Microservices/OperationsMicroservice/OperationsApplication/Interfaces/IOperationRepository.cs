using Operations.Application.Settings;
using Operations.Core.Entities;

namespace Operations.Application.Interfaces
{
    public interface IOperationRepository
    {
        Task CreateAsync(Operation operation, CancellationToken cancellationToken);
        Task DeleteAsync(string objectId, CancellationToken cancellationToken);
        Task<Operation> GetAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<Operation>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<IEnumerable<Operation>> GetByAccountIdAsync(int accountId, PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}
