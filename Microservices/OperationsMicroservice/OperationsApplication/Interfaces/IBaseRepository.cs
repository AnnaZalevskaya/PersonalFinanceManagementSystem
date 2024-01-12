using Operations.Application.Settings;

namespace Operations.Application.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);
    }
}
