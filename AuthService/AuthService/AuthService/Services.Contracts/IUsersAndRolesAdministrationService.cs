using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.BO;

namespace AuthService.Services.Contracts
{
    public interface IUsersAndRolesAdministrationService
    {
        Task<bool> CreateNewUser(ApplicationUser user, string password);

        Task<bool> CreateNewRole(string roleName);

        Task<bool> AssignRoleToUser(string roleName, string userId);

        Task<List<UserWithRole>> GetAllUsers();
    }
}