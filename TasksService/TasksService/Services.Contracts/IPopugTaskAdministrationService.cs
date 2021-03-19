using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksService.BO;

namespace TasksService.Services.Contracts
{
    public interface IPopugTaskAdministrationService
    {
        Task<List<PopugTask>> GetByAssignee(Guid assigneeId);
        Task<PopugTask> CreateNew(PopugTask newTask);
        Task<PopugTask> AssignToUser(Guid taskId, Guid userId);
        Task<PopugTask> ClosePopugTask(Guid taskId);
        Task<PopugTask> ReopenPopugTask(Guid taskId);
    }
}