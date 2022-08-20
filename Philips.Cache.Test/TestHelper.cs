using Entity.Manager;
using Philip.Memory.Cache;
using Philip.Memory.Cache.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Cache.Test
{
    public static class TestHelper
    {
        public static UserModel GetUserModel()
        {
            UserModel user = new UserModel()
            {
                Name = "Sambeet",
                Address = "Bangalore"
            };

            return user;
        }

        public static OptionModel GetOptionModel_SlidingExpiration_Min(long value)
        {
            OptionModel option = new OptionModel()
            {
                SlidingExpiration = TimeSpan.FromMinutes(value)
            };

            return option;
        }

        public static OptionModel GetOptionModel_SlidingExpiration_Sec(long value)
        {
            OptionModel option = new OptionModel()
            {
                SlidingExpiration = TimeSpan.FromSeconds(value)
            };

            return option;
        }
    }
}
