using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Operations.Application.Interfaces;
using Operations.Application.Settings;
using Operations.Infrastructure.Settings;
using System.Text;
using System.Text.Json;

namespace Operations.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly CacheSettings _cacheOptions;

        public CacheRepository(IDistributedCache distributedCache, IOptions<CacheSettings> cacheOptions)
        {
            _distributedCache = distributedCache;
            _cacheOptions = cacheOptions.Value;
        }

        public async Task CacheDataAsync<T>(string id, T value)
        {
            string cacheKey = $"Data_{id}";

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(value);
            await _distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(jsonData), options);
        }

        public async Task<T> GetCachedDataAsync<T>(string id)
        {
            string cacheKey = $"Data_{id}";
            byte[] cachedData = await _distributedCache.GetAsync(cacheKey);

            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(cachedData));
            }
            else
            {
                await _distributedCache.RemoveAsync(cacheKey);

                return default;
            }
        }

        public async Task CacheLargeDataAsync<T>(PaginationSettings paginationSettings, List<T> data, string accountId = "")
        {
            var cacheKey = GenerateCacheKey(paginationSettings, accountId);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(data);
            await _distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(jsonData), options);
        }

        public async Task<List<T>> GetCachedLargeDataAsync<T>(PaginationSettings paginationSettings, string accountId = "")
        {
            var cacheKey = GenerateCacheKey(paginationSettings, accountId);

            var dataBytes = await _distributedCache.GetAsync(cacheKey);

            if (dataBytes == null)
            {
                return new List<T>();
            }

            var jsonData = Encoding.UTF8.GetString(dataBytes);
            var data = JsonSerializer.Deserialize<List<T>>(jsonData);

            return data;
        }

        public async Task RemoveCachedDataAsync(string id)
        {
            string cacheKey = $"Data_{id}";
            var data = await _distributedCache.GetAsync(cacheKey);

            if (data != null)
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }
        }

        private string GenerateCacheKey(PaginationSettings paginationSettings, string accountId)
        {
            return $"{_cacheOptions.KeyPrefix}_{accountId}_{paginationSettings.PageNumber}_{paginationSettings.PageSize}";
        }
    }
}
