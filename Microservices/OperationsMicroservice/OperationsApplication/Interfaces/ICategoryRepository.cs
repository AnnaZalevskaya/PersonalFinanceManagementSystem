using Operations.Application.Settings;
using Operations.Core.Entities;

namespace Operations.Application.Interfaces
{
    public interface ICategoryRepository
    {

        Task<Category> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Category>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}
