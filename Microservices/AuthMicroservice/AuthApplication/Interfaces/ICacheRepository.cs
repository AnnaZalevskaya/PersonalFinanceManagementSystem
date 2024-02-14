namespace Auth.Application.Interfaces
{
    public interface ICacheRepository
    {
        Task<T> GetDataCacheAsync<T>(long id);
        Task SetDataCacheAsync<T>(long id, T value);
        Task RemoveDataCacheAsync(long id);
    }
}
