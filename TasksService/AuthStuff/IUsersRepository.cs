using System.Threading.Tasks;

namespace AuthStuff
{
    public interface IUsersRepository
    {
        Task AddUser(User user);
        Task AddRoleToUser(string userId, string roleName);
    }
}