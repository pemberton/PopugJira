using System;
using System.Threading.Tasks;
using TasksService.BO;

namespace TasksService.Repositories.Contracts
{
    public interface IUsersRepository
    {
        Task<User> GetById(Guid userId);
    }
}