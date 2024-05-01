using Auth.Application.Settings;

namespace Auth.Application.Interfaces
{
    public interface ICacheRepository
    {
        Task CacheLargeDataAsync<T>(PaginationSettings paginationSettings, List<T> data);
        Task<List<T>> GetCachedLargeDataAsync<T>(PaginationSettings paginationSettings);
    }
}
