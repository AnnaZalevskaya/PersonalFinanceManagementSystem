namespace Accounts.DataAccess.Repositories.Interfaces
{
    public interface ICacheRepository
    {
        Task<T> GetDataCacheAsync<T>(int id);
        Task SetDataCacheAsync<T>(int id, T value);
        Task UpdateDataCacheAsync<T>(int id, T value);
        Task RemoveDataCacheAsync(int id);
    }
}
