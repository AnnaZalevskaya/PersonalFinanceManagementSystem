namespace Operations.Application.Interfaces
{
    public interface ICacheRepository
    {
        Task<T> GetDataCacheAsync<T>(string id);
        Task SetDataCacheAsync<T>(string id, T value);
        Task RemoveDataCacheAsync(string id);
    }
}
