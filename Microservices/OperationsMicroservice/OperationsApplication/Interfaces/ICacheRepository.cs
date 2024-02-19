using Operations.Application.Settings;

namespace Operations.Application.Interfaces
{
    public interface ICacheRepository
    {
        Task CacheDataAsync<T>(string id, T value);
        Task<T> GetCachedDataAsync<T>(string id);
        Task CacheLargeDataAsync<T>(PaginationSettings paginationSettings, List<T> data, string accountId = "");
        Task<List<T>> GetCachedLargeDataAsync<T>(PaginationSettings paginationSettings, string accountId = "");
        Task RemoveCachedDataAsync(string id);
    }
}
