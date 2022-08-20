using Entity.Manager;
using Microsoft.Extensions.Caching.Memory;
using Philip.Memory.Cache.Contract;
using System.Collections;
using System.Reflection;

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
            if (cacheKey == null)
                throw new ArgumentNullException(nameof(cacheKey));

            var result = _memoryCache.Get<T>(cacheKey);
            return result;
        }

        public async Task Set<T>(string cacheKey, T cacheValue, OptionModel options)
        {
            if (cacheKey == null)
                throw new ArgumentNullException(nameof(cacheKey));

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            if (!options.Equals(null))
            {
                if (options.SlidingExpiration.HasValue)
                {
                    cacheEntryOptions.SetSlidingExpiration(options.SlidingExpiration.Value);
                }
                else if (options.AbsoluteExpiration.HasValue)
                {
                    cacheEntryOptions.SetAbsoluteExpiration(options.AbsoluteExpiration.Value);
                }
            }
            _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
        }

        public async Task<dynamic> GetAll()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                var collection = field.GetValue(_memoryCache) as ICollection;
                if (collection != null)
                    return collection;
            }
            return null;
        }
    }
}