using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UsersAdministrationService : IUsersAdministrationService
    {
        private readonly UserManager<ApplicationUser> _userManager = null;

        public UsersAdministrationService(
            UserManager<ApplicationUser> usersRepository)
        {
            _userManager = usersRepository;
        }

        public async Task<bool> CreateNew(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public Task<List<ApplicationUser>> GetAll()
        {
            return _userManager.Users.ToListAsync();
        }
    }
}