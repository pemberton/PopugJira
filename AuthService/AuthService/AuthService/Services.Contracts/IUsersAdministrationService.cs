using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.BO;

namespace AuthService.Services.Contracts
{
    public interface IUsersAdministrationService
    {
        Task<bool> CreateNew(ApplicationUser user, string password);

        Task<List<ApplicationUser>> GetAll();
    }
}