using System;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Host.Dto;
using AuthService.Host.Dto.AuthService;
using AuthService.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.Host.Controllers
{
    [ApiController]
    [Route("api/auth")]

    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly SignInManager<ApplicationUser> _signInManager = null;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<LoginController> _logger;
        private readonly IUsersAndRolesAdministrationService service;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtGenerator jwtGenerator,
            ILogger<LoginController> logger,
            IUsersAndRolesAdministrationService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;

            _logger = logger;
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public async Task<UserLoginDto> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new MethodAccessException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            var userWithRole = await service.GetUserWithRole(user);

            if (result.Succeeded)
                return new UserLoginDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user),
                    Role = userWithRole.Role
                };

            throw new MethodAccessException();
        }

        [HttpGet]
        [Route("logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}