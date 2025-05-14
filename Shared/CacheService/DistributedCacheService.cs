using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CacheService
{
    public class DistributedCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetCachedResponseAsync<T>(string key)
        {
            var cachedDataBytes = await _cache.GetAsync(key);
            if (cachedDataBytes != null)
            {
                return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(cachedDataBytes));
            }

            return default(T);
        }

        public async Task UpsertCachedResponseAsync<T>(string key, T data, TimeSpan? expiration = null)
        {
            if (expiration == null)
                expiration = new TimeSpan(0, 60, 0);

            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration.Value
            };
            var dataBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
            await _cache.SetAsync(key, dataBytes, cacheEntryOptions);

            return;
        }

        public async Task RemoveCacheKeyAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
