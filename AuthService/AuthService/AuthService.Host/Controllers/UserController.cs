using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Host.Dto;
using AuthService.Host.Dto.AuthService;
using AuthService.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Host.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private IUsersAndRolesAdministrationService _usersAdministrationService;

        public UserController(IUsersAndRolesAdministrationService usersAdministrationService)
        {
            _usersAdministrationService = usersAdministrationService;
        }

        [HttpPut]
        [Route("users")]
        public async Task<IActionResult> AddUser([FromBody]UserCreateNewDto user)
        {
            var applicationUser = new ApplicationUser {UserName = user.UserName, Email = user.Email};
            var isCreated = await _usersAdministrationService.CreateNewUser(applicationUser, user.Password);

            if (isCreated)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("roles")]
        public async Task<IActionResult> AddRole([FromBody]RoleCreateNewDto role)
        {
            var isCreated = await _usersAdministrationService.CreateNewRole(role.RoleName);

            if (isCreated)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("roles/{roleName}/users/{userId}")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleName)
        {
            var isAssigned = await _usersAdministrationService.AssignRoleToUser(roleName, userId);

            if (isAssigned)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<List<UserGetAllDto>> GetAll()
        {
            var users = await _usersAdministrationService.GetAllUsers();
            return users
                .Select(au => new UserGetAllDto {Id =au.Id, Email = au.Email, UserName = au.UserName, Role = au.Role})
                .ToList();
        }
    }
}