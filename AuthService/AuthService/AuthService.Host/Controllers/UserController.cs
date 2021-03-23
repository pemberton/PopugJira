using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Host.Dto;
using AuthService.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Host.Controllers
{
    [ApiController]
    [Route("api/auth/users")]
    public class UserController : ControllerBase
    {
        private IUsersAdministrationService _administrationService;

        public UserController(IUsersAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody]UserCreateNewDto user)
        {
            var applicationUser = new ApplicationUser {UserName = user.UserName, Email = user.Email};
            var isCreated = await _administrationService.CreateNew(applicationUser, user.Password);

            if (isCreated)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<List<UserGetAllDto>> GetAll()
        {
            var users = await _administrationService.GetAll();
            return users
                .Select(au => new UserGetAllDto {Email = au.Email, UserName = au.UserName})
                .ToList();
        }
    }
}