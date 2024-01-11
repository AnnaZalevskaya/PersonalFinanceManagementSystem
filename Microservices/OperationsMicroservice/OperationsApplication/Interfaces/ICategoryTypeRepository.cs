using Operations.Application.Settings;
using Operations.Core.Entities;

namespace Operations.Application.Interfaces
{
    public interface ICategoryTypeRepository
    {
        Task<CategoryType> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryType>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}
