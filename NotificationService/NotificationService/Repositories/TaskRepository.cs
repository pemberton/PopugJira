using System;
using System.Threading.Tasks;
using NotificationService.BO;
using NotificationService.Repositories.Contracts;

namespace NotificationService.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        public Task<PopugTask> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}