using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Operations.Application.Interfaces;
using Operations.Infrastructure.Settings;
using System.Text;
using System.Text.Json;

namespace Operations.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IOptions<CacheSettings> _cacheOptions;

        public CacheRepository(IDistributedCache distributedCache, IOptions<CacheSettings> cacheOptions)
        {
            _distributedCache = distributedCache;
            _cacheOptions = cacheOptions;
        }

        public async Task CacheDataAsync<T>(string id, T value)
        {
            string cacheKey = $"Data_{id}";

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.Value.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = _cacheOptions.Value.SlidingExpiration
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

        public async Task CacheLargeDataAsync<T>(string id, List<T> data)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < data.Count; i++)
            {
                string cacheKey = $"{id}_{i}";

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheOptions.Value.AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = _cacheOptions.Value.SlidingExpiration
                };

                var jsonData = JsonSerializer.Serialize(data[i]);
                tasks.Add(_distributedCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(jsonData), options));
            }

            var indexKey = $"index_{id}";
            var indexData = data.Select((_, i) => $"{id}_{i}").ToArray();
            var indexJsonData = JsonSerializer.Serialize(indexData);
            await _distributedCache.SetAsync(indexKey, Encoding.UTF8.GetBytes(indexJsonData));

            await Task.WhenAll(tasks);
        }

        public async Task<List<T>> GetCachedLargeDataAsync<T>(string id)
        {
            var indexKey = $"index_{id}";
            var indexBytes = await _distributedCache.GetAsync(indexKey);

            if (indexBytes == null)
            {
                return new List<T>();
            }

            var indexJsonData = Encoding.UTF8.GetString(indexBytes);
            var indexData = JsonSerializer.Deserialize<string[]>(indexJsonData);

            var tasks = new List<Task<byte[]>>();
            var results = new List<T>();

            foreach (var key in indexData)
            {
                tasks.Add(_distributedCache.GetAsync(key));
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                var bytes = await task;

                if (bytes == null)
                {
                    continue;
                }

                var jsonData = Encoding.UTF8.GetString(bytes);
                var item = JsonSerializer.Deserialize<T>(jsonData);
                results.Add(item);
            }

            return results;
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
    }
}
