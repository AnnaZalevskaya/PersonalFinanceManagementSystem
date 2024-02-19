namespace Auth.Application.Interfaces
{
    public interface ICacheRepository
    {
        Task CacheLargeDataAsync<T>(string id, List<T> data);
        Task<List<T>> GetCachedLargeDataAsync<T>(string id);
    }
}
