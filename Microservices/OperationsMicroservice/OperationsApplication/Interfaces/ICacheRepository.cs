namespace Operations.Application.Interfaces
{
    public interface ICacheRepository
    {
        Task CacheDataAsync<T>(string id, T value);
        Task<T> GetCachedDataAsync<T>(string id);
        Task CacheLargeDataAsync<T>(string id, List<T> data);
        Task<List<T>> GetCachedLargeDataAsync<T>(string id);
        Task RemoveCachedDataAsync(string id);
    }
}
