using System;
using System.Threading.Tasks;

namespace NotificationService.Services.Contracts
{
    public interface INotificationSender
    {
        Task NotifyUserAboutNewTask(Guid userId, Guid taskId);
    }
}