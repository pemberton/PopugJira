using System;
using System.Threading.Tasks;
using NotificationService.BO;

namespace NotificationService.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
    }
}