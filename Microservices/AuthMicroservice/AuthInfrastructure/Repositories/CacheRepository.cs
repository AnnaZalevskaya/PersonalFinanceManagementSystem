using Auth.Application.Interfaces;
using Auth.Application.Settings;
using Auth.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Auth.Infrastructure.Repositories
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

        public async Task CacheLargeDataAsync<T>(PaginationSettings paginationSettings, List<T> data)
        {
            var cacheKey = GenerateCacheKey(paginationSettings);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(data);
            await _distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(jsonData), options);
        }

        public async Task<List<T>> GetCachedLargeDataAsync<T>(PaginationSettings paginationSettings)
        {
            var cacheKey = GenerateCacheKey(paginationSettings);

            var dataBytes = await _distributedCache.GetAsync(cacheKey);

            if (dataBytes == null)
            {
                return new List<T>();
            }

            var jsonData = Encoding.UTF8.GetString(dataBytes);
            var data = JsonSerializer.Deserialize<List<T>>(jsonData);

            return data;
        }

        private string GenerateCacheKey(PaginationSettings paginationSettings)
        {
            return $"{_cacheOptions.KeyPrefix}_{paginationSettings.PageNumber}_{paginationSettings.PageSize}";
        }
    }
}
