using Microsoft.AspNetCore.Mvc;

namespace AuthService.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet]
        public string Test()
        {
            return "test";
        }
    }
}