using Accounts.DataAccess.Settings;

namespace Accounts.DataAccess.Repositories.Interfaces
{
    public interface ICacheRepository
    {
        Task CacheDataAsync<T>(int id, T value);
        Task<T> GetCachedDataAsync<T>(int id);
        Task CacheLargeDataAsync<T>(PaginationSettings paginationSettings, List<T> data, string userId = "");
        Task<List<T>> GetCachedLargeDataAsync<T>(PaginationSettings paginationSettings, string userId = "");      
        Task RemoveCachedDataAsync(int id);
    }
}
