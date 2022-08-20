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
        public void Set_Get()
        {
            var userModel = TestHelper.GetUserModel();
            string cacheKey = "test_key";
            var option = TestHelper.GetOptionModel_SlidingExpiration_Min(2);

            _cache.Set<UserModel>(cacheKey, userModel, option);

            var result = _cache.Get<UserModel>(cacheKey);

            Assert.NotNull(result.Result);
            Assert.True(userModel.Equals(result.Result));
        }
    }
}