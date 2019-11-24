using Domain;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
