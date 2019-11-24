using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Domain;

namespace Services
{
    public class UserService : IUserService
    {
        public const string ReadRoleName = "read";
        public const string WriteRoleName = "write";

        private static IList<User> Users = new List<User>
        {
            new User { UserName = "test", Password = "test", Roles = new List<string> {   ReadRoleName , WriteRoleName  }  },
            new User { UserName = "reader", Password = "reader", Roles = new List<string> { ReadRoleName }  },
            new User { UserName = "writer", Password = "writer", Roles = new List<string> { WriteRoleName }  },
        };

        public async Task<User> Authenticate(string username, string password)
        {
            return await Task.Run(() => Users.FirstOrDefault(i => i.UserName == username && i.Password == password));
        }
    }
}
