using System;
using System.Threading.Tasks;
using AccountingService.Repositories.Contracts;

namespace AccountingService.Services
{
    public class AccountService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountService(
            ITaskRepository taskRepository,
            IAccountRepository accountRepository)
        {
            _taskRepository = taskRepository;
            _accountRepository = accountRepository;
        }

        public async Task WriteOff(Guid taskId, Guid assigneeId)
        {
            var account = await _accountRepository.GetByUserId(assigneeId);
            if (account == null)
                throw new ArgumentException(nameof(assigneeId));

            var task = await _taskRepository.GetById(taskId);
            if (task == null)
                throw new ArgumentException(nameof(taskId));

            account.Balance -= task.AssignCost;

            // сообщить, что деньги списаны со счета
        }
    }
}