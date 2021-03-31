using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Services.Contracts;
using AuthService.Streams.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UsersAdministrationService : IUsersAdministrationService
    {
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly IUserBusinessEventsStream _businessEventsStream;

        public UsersAdministrationService(
            UserManager<ApplicationUser> usersRepository,
            IUserBusinessEventsStream businessEventsStream)
        {
            _userManager = usersRepository;
            _businessEventsStream = businessEventsStream;
        }

        public async Task<bool> CreateNew(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(user.Email);
                await _businessEventsStream.UserWasCreated(newUser);
            }

            return result.Succeeded;
        }

        public Task<List<ApplicationUser>> GetAll()
        {
            return _userManager.Users.ToListAsync();
        }
    }
}