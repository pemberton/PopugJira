using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Services.Contracts;
using AuthService.Streams.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UsersAndRolesAdministrationService : IUsersAndRolesAdministrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserBusinessEventsStream _businessEventsStream;

        public UsersAndRolesAdministrationService(
            UserManager<ApplicationUser> usersRepository,
            RoleManager<IdentityRole> roleRepository,
            IUserBusinessEventsStream businessEventsStream)
        {
            _userManager = usersRepository;
            _roleManager = roleRepository;
            _businessEventsStream = businessEventsStream;
        }

        public async Task<bool> CreateNewUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(user.Email);
                await _businessEventsStream.UserWasCreated(newUser);
            }

            return result.Succeeded;
        }

        public async Task<bool> CreateNewRole(string roleName)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                var newRole = await _roleManager.FindByNameAsync(roleName);
                await _businessEventsStream.RoleWasCreated(newRole.Id, newRole.Name);
            }

            return roleResult.Succeeded;
        }

        public async Task<bool> AssignRoleToUser(string roleName, string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (currentUser == null || role == null)
                throw new ArgumentException("Invalid userId or roleId");

            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);

            await _userManager.RemoveFromRolesAsync(currentUser, currentUserRoles);

            var roleResult = await _userManager.AddToRoleAsync(currentUser, role.Name);

            if (roleResult.Succeeded)
            {
                await _businessEventsStream.UserWasGrantedToRole(currentUser.Id, role.Name);
            }

            return roleResult.Succeeded;
        }

        public async Task<List<UserWithRole>> GetAllUsers()
        {
            var allUsers = await _userManager.Users.ToListAsync();

            var result = new List<UserWithRole>();
            foreach (var user in allUsers)
            {
                var userWithRole = await GetUserWithRole(user);
                result.Add(userWithRole);
            }

            return result;
        }

        public async Task<UserWithRole> GetUserWithRole(ApplicationUser user)
        {
            var role = await _userManager.GetRolesAsync(user);
            var userWithRole = new UserWithRole
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = role.Count > 0 ? role[0] : null
            };

            return userWithRole;
        }
    }
}