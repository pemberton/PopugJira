using System;
using System.Threading.Tasks;
using NotificationService.BO;

namespace NotificationService.Repositories.Contracts
{
    public interface ITaskRepository
    {
        Task<PopugTask> GetById(Guid id);
    }
}