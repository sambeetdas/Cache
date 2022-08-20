using Entity.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Philip.Memory.Cache.Contract;

namespace Cache.Execute.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICacheHandler _cache;
        public UserController(ICacheHandler cache)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<UserModel> Set(UserModel user)
        {
            OptionModel option = new OptionModel()
            {
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            await _cache.Set<UserModel>(user.Name.ToLower(), user, option);
            return user;
        }

        [HttpGet]
        public async Task<UserModel> Get(string name)
        {
            var result =  await _cache.Get<UserModel>(name.ToLower());
            return result;
        }

        [HttpGet]
        public async Task<List<UserModel>> GetAll()
        {
            var result = await _cache.GetAll();
            return result;
        }
    }
}
