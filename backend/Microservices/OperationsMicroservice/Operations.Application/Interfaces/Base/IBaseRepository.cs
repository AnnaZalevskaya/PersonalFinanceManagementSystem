using Operations.Application.Settings;

namespace Operations.Application.Interfaces.Base
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken);
        Task<long> GetRecordsCountAsync();
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);
    }
}
