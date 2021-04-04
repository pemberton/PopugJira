using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Host.Dto.TaskServiceDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Host.Controllers
{
    [Route("api/tasks")]
    public class TaskApiGatewayController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        private static readonly string taskApiUrl = "http://localhost:5001/api/tasks";
        private readonly UserManager<ApplicationUser> _userManager = null;

        public TaskApiGatewayController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<PopugTaskDto>> GetAll()
        {
            var response = await client.GetAsync(taskApiUrl);
            response.EnsureSuccessStatusCode();

            return await Deserialize<List<PopugTaskDto>>(response);
        }

        [HttpPut]
        public async Task Add([FromBody]PopugTaskDto task)
        {
            var serialized = JsonSerializer.Serialize(task);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            var email = _userManager.GetUserId(User);
            var appUser = await _userManager.FindByNameAsync(email);
            var response = await client.PutAsync($"{taskApiUrl}/{appUser.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        // TODO: проверить по роли, что менеджер
         [HttpPost]
         [Route("assign")]
         public async Task AssignTasks()
         {
             var email = _userManager.GetUserId(User);
             var appUser = await _userManager.FindByNameAsync(email);
             var response = await client.PostAsync($"{taskApiUrl}/{appUser.Id}/assign", null);
             response.EnsureSuccessStatusCode();
         }

        // [HttpPost]
        // [Route("{taskId}/close")]
        // public async Task Close(Guid taskId)
        // {
        //     await _taskService.ClosePopugTaskDto(taskId);
        // }
        //
        // [HttpPost]
        // [Route("{taskId}/reopen")]
        // public async Task Reopen(Guid taskId)
        // {
        //     await _taskService.ReopenPopugTaskDto(taskId);
        // }

        private static async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var tasks = JsonSerializer.Deserialize<T>(responseBody, options);
            return tasks;
        }


    }
}