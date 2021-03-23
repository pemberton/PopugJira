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
    [Route("api/[controller]")]
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
        public async Task<IEnumerable<PopugTask>> GetAll()
        {
            return await _taskService.GetAll();
        }

        [HttpGet]
        [Route("{taskId}")]
        public async Task<PopugTask> GetById(Guid taskId)
        {
            return await _taskService.GetById(taskId);
        }

        [HttpGet]
        [Route("assignee/{assigneeId}")]
        public async Task<IEnumerable<PopugTask>> Get(Guid assigneeId)
        {
            return await _taskService.GetByAssignee(assigneeId);
        }

        [HttpPut]
        [Route("{creatorId}")]
        public async Task Add(Guid creatorId, PopugTask task)
        {
            await _taskService.CreateNew(creatorId, task);
        }

        [HttpPost]
        [Route("{actorId}/{taskId}/assignTo/{userId}")]
        public async Task AssignToUser(Guid actorId, Guid taskId, Guid userId)
        {
            await _taskService.AssignToUser(actorId, taskId, userId);
        }
        
        [HttpPost]
        [Route("{actorId}/{taskId}/close")]
        public async Task Close(Guid actorId, Guid taskId)
        {
            await _taskService.ClosePopugTask(actorId, taskId);
        }
    }
}