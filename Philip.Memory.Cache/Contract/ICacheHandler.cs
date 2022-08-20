using Entity.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philip.Memory.Cache.Contract
{
    public interface ICacheHandler
    {
        Task<T> Get<T>(string cacheKey);
        Task Set<T>(string cacheKey, T cacheValue, OptionModel options);
        Task<dynamic> GetAll();
    }
}
