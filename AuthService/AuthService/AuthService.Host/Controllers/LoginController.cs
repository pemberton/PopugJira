using System;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Host.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace AuthService.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly SignInManager<ApplicationUser> _signInManager = null;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<LoginController> _logger;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtGenerator jwtGenerator,
            ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;

            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                throw new MethodAccessException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
                return new UserDto
                {
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName
                };

            throw new MethodAccessException();
        }

        [HttpGet]
        [Route("some")]
        public void LoginAgain()
        {}
    }
}