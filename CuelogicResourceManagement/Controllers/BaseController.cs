using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CuelogicResourceManagement.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        public IMemoryCache _cache;
        public BaseController(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T Get(string cacheKey)
        {
            T result = (T)_cache.Get(cacheKey);
            return result;
        }

        public void Add(string cachekey, T value)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(20)
            };
            _cache.Set(cachekey, value, cacheExpiryOptions);
        }

        public void Delete(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }
}
