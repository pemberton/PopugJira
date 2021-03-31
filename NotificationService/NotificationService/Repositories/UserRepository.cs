using System;
using System.Threading.Tasks;
using NotificationService.BO;
using NotificationService.Repositories.Contracts;

namespace NotificationService.Repositories
{
    public class UserRepository:IUserRepository
    {
        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}