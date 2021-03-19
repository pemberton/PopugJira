using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasksService.BO;
using TasksService.Services.Contracts;

namespace TasksService.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly IPopugTaskAdministrationService _taskService;

        public TasksController(
            ILogger<TasksController> logger,
            IPopugTaskAdministrationService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IEnumerable<PopugTask>> Get(Guid assigneeId)
        {
            return await _taskService.GetByAssignee(assigneeId);
        }

        [HttpPut]
        public async Task Add(PopugTask task)
        {
            await _taskService.CreateNew(task);
        }

        [HttpPost]
        [Route("{taskId}/assignTo/{userId}")]
        public async Task AssignToUser(Guid taskId, Guid userId)
        {
            await _taskService.AssignToUser(taskId, userId);
        }
        
        [HttpPost]
        [Route("{taskId}/close")]
        public async Task Close(Guid taskId)
        {
            await _taskService.ClosePopugTask(taskId);
        }
        
        [HttpPost]
        [Route("{taskId}/reopen")]
        public async Task Reopen(Guid taskId)
        {
            await _taskService.ReopenPopugTask(taskId);
        }
    }
}