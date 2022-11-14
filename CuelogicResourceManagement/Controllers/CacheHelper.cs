using Microsoft.Extensions.Caching.Memory;

namespace CuelogicResourceManagement.Controllers
{
    public class CacheHelper:ICacheHelper
    {
        public IMemoryCache _cache;
        public CacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string cacheKey)
        {
            T result = (T)_cache.Get(cacheKey);
            return result;
        }

        public void Add<T>(string cachekey, T value)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(20)
                
            };
            _cache.Set(cachekey, value, cacheExpiryOptions);
        }

        public void Delete(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }

    public interface ICacheHelper
    {
        T Get<T>(string cacheKey);
        void Add<T>(string cachekey, T value);
        void Delete(string cacheKey);
    }
}
