using System;
using System.Threading.Tasks;
using AccountingService.BO;

namespace AccountingService.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task<Account> GetByUserId(Guid userId);
        Task AddOrUpdate(Account account);
    }
}