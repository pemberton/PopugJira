using System.Threading.Tasks;
using AuthService.BO;

namespace AuthService.Streams.Contracts
{
    public interface IUserBusinessEventsStream
    {
        Task UserWasCreated(ApplicationUser user);

        Task RoleWasCreated(string id, string name);

        Task UserWasGrantedToRole(string userId, string roleName);
    }
}