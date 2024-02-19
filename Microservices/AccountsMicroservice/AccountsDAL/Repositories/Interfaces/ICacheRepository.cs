namespace Accounts.DataAccess.Repositories.Interfaces
{
    public interface ICacheRepository
    {
        Task CacheDataAsync<T>(int id, T value);
        Task<T> GetCachedDataAsync<T>(int id);
        Task CacheLargeDataAsync<T>(string id, List<T> data);
        Task<List<T>> GetCachedLargeDataAsync<T>(string id);      
        Task RemoveCachedDataAsync(int id);
    }
}
