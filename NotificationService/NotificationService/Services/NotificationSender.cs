using System;
using System.Threading.Tasks;
using NotificationService.Repositories.Contracts;
using NotificationService.Services.Contracts;

namespace NotificationService.Services
{
    public class NotificationSender : INotificationSender
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public NotificationSender(
            ITaskRepository taskRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task NotifyUserAboutNewTask(Guid userId, Guid taskId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
                throw new ArgumentException(nameof(userId));

            var task = await _taskRepository.GetById(taskId);
            if (task == null)
                throw new ArgumentException(nameof(taskId));

            // send email
            Console.Write($"Dear {user.Name}! You've been assinged to new task {task.Description}! Star to do it immediately!");
        }
    }
}