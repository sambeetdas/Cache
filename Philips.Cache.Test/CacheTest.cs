using Entity.Manager;
using Microsoft.Extensions.Caching.Memory;
using Philip.Memory.Cache;
using Philip.Memory.Cache.Contract;
using Xunit.Extensions.AssemblyFixture;

namespace Philips.Cache.Test
{
    public class CacheTest
    {
        private readonly ICacheHandler _cache;
        public CacheTest(ICacheHandler cache)
        {
            _cache = cache;
        }

        [Fact]
        public void Set_Get_SlidingExpiration()
        {
            var userModel = TestHelper.GetUserModel();
            string cacheKey = "Set_Get_SlidingExpiration";
            var option = TestHelper.GetOptionModel_SlidingExpiration_Min(2);

            _cache.Set<UserModel>(cacheKey, userModel, option);

            var result = _cache.Get<UserModel>(cacheKey);

            Assert.NotNull(result.Result);
            Assert.True(userModel.Equals(result.Result));
        }

        [Fact]
        public void Set_Get_AbsoluteExpiration()
        {
            var userModel = TestHelper.GetUserModel();
            string cacheKey = "Set_Get_AbsoluteExpiration";
            var option = TestHelper.GetOptionModel_AbsoluteExpiration(DateTime.Now.AddMinutes(2));

            _cache.Set<UserModel>(cacheKey, userModel, option);

            var result = _cache.Get<UserModel>(cacheKey);

            Assert.NotNull(result.Result);
            Assert.True(userModel.Equals(result.Result));
        }

        [Fact]
        public void Cache_SlidingExpiration()
        {
            var userModel = TestHelper.GetUserModel();
            string cacheKey = "Cache_SlidingExpiration";
            var option = TestHelper.GetOptionModel_SlidingExpiration_Sec(2);

            _cache.Set<UserModel>(cacheKey, userModel, option);

            System.Threading.Thread.Sleep(2001);

            var result = _cache.Get<UserModel>(cacheKey);

            Assert.Null(result.Result);
        }

        [Fact]
        public void Cache_AbsoluteExpiration()
        {
            var userModel = TestHelper.GetUserModel();
            string cacheKey = "Cache_AbsoluteExpiration";
            var option = TestHelper.GetOptionModel_AbsoluteExpiration(DateTime.Now.AddSeconds(2));

            _cache.Set<UserModel>(cacheKey, userModel, option);

            System.Threading.Thread.Sleep(2001);

            var result = _cache.Get<UserModel>(cacheKey);

            Assert.Null(result.Result);
        }
    }
}