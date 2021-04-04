using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using TasksService.BO;
using TasksService.Services.Contracts;

namespace TasksService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IPopugTaskAdministrationService _taskService;

        public TasksController(
            IPopugTaskAdministrationService taskService)
        {
            _taskService = taskService;
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
        [Route("{actorId}/assign")]
        public async Task AssignTasks(Guid actorId)
        {
            await _taskService.AssignTasks(actorId);
        }
        
        [HttpPost]
        [Route("{actorId}/{taskId}/close")]
        public async Task Close(Guid actorId, Guid taskId)
        {
            await _taskService.ClosePopugTask(actorId, taskId);
        }
    }
}