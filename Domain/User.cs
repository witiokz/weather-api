using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
