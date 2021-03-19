using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksService.BO;

namespace TasksService.Repositories.Contracts
{
    public interface IPopugTaskRepository
    {
        Task<PopugTask> GetById(Guid popugTaskId);
        Task<List<PopugTask>> GetByAssignee(Guid assigneeId);
        Task<PopugTask> AddOrUpdate(PopugTask newTask);
    }
}