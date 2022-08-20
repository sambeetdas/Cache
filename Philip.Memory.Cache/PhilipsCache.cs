using Entity.Manager;
using Microsoft.Extensions.Caching.Memory;
using Philip.Memory.Cache.Contract;

namespace Philip.Memory.Cache
{
    public class PhilipsCache : ICacheHandler
    {
        private readonly IMemoryCache _memoryCache;
        public PhilipsCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<T> Get<T>(string cacheKey)
        {
            var result = _memoryCache.Get<T>(cacheKey);
            return result;
        }

        public async Task Set<T>(string cacheKey, T cacheValue, OptionModel options)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            if (!options.Equals(null))
            {
                if (options.SlidingExpiration.HasValue)
                {
                    cacheEntryOptions.SetSlidingExpiration(options.SlidingExpiration.Value);
                }                
            }
            _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
        }
    }
}