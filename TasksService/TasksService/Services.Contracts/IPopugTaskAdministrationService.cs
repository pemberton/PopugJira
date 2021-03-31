using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksService.BO;

namespace TasksService.Services.Contracts
{
    public interface IPopugTaskAdministrationService
    {
        Task<List<PopugTask>> GetAll();
        Task<List<PopugTask>> GetByAssignee(Guid assigneeId);
        Task<PopugTask> CreateNew(Guid creatorId, PopugTask newTask);
        Task AssignTasks(Guid actorId);
        Task<PopugTask> ClosePopugTask(Guid actorId,  Guid taskId);
        Task<PopugTask> GetById(Guid taskId);
    }
}