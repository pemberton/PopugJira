using System;
using System.Threading.Tasks;
using AccountingService.BO;

namespace AccountingService.Repositories.Contracts
{
    public interface ITaskRepository
    {
        Task<PopugTask> GetById(Guid id);
    }
}