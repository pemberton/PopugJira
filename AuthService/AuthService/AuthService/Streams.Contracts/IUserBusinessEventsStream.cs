using System.Threading.Tasks;
using AuthService.BO;

namespace AuthService.Streams.Contracts
{
    public interface IUserBusinessEventsStream
    {
        Task UserWasCreated(ApplicationUser user);
    }
}