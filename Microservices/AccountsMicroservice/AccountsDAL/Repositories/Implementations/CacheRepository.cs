using Accounts.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Accounts.DataAccess.Repositories.Implementations
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public CacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetDataCacheAsync<T>(int id)
        {
            string cacheKey = $"Data_{id}";
            var data = await _distributedCache.GetAsync(cacheKey);

            if (data == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetDataCacheAsync<T>(int id, T value)
        {
            string cacheKey = $"Data_{id}";

            if (string.IsNullOrEmpty(await _distributedCache.GetStringAsync(cacheKey)))
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(500),
                    SlidingExpiration = TimeSpan.FromSeconds(500)
                };

                var jsonData = JsonSerializer.Serialize(value);
                await _distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(jsonData), options);
            }
        }

        public async Task UpdateDataCacheAsync<T>(int id, T value)
        {
            string cacheKey = $"Data_{id}";
            var jsonData = JsonSerializer.SerializeToUtf8Bytes(value);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(500),
                SlidingExpiration = TimeSpan.FromSeconds(500)
            };

            await _distributedCache.SetAsync(cacheKey, jsonData, options);
        }

        public async Task RemoveDataCacheAsync(int id)
        {
            string cacheKey = $"Data_{id}";
            var data = await _distributedCache.GetAsync(cacheKey);

            if (data != null)
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }
        }
    }
}
