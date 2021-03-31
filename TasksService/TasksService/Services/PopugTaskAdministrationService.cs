using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksService.BO;
using TasksService.Repositories.Contracts;
using TasksService.Services.Contracts;
using TasksService.Streams.Contracts;

namespace TasksService.Services
{
    public class PopugTaskAdministrationService : IPopugTaskAdministrationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPopugTaskRepository _tasksRepository;
        private readonly ITaskBusinessEventsStream _businessEventsStream;

        public PopugTaskAdministrationService(
            IUsersRepository usersRepository,
            IPopugTaskRepository tasksRepository,
            ITaskBusinessEventsStream businessEventsStream)
        {
            _usersRepository = usersRepository;
            _tasksRepository = tasksRepository;
            _businessEventsStream = businessEventsStream;
        }

        public Task<List<PopugTask>> GetAll()
        {
            return _tasksRepository.GetAll();
        }

        public Task<List<PopugTask>> GetByAssignee(Guid assigneeId)
        {
            return _tasksRepository.GetByAssignee(assigneeId);
        }

        public async Task<PopugTask> CreateNew(Guid creatorId, PopugTask newTask)
        {
            var creator = await _usersRepository.GetById(creatorId);
            var createdTask = PopugTask.CreateNew(newTask, creator);
            var savedTask = await _tasksRepository.AddOrUpdate(createdTask);

            await _businessEventsStream.StreamAboutTaskCreated(savedTask);
            return savedTask;
        }

        public async Task AssignTasks(Guid actorId)
        {
            // проверить, что ассайнит менеджер

            var tasks = await _tasksRepository.GetAll();

            var users = await _usersRepository.GetAll();

            foreach (var task in tasks)
            {
                task.AssignToRandomUser(users);
                await _tasksRepository.AddOrUpdate(task);

                await _businessEventsStream.StreamAboutTaskAssigned(task);
            }
        }
        
        public async Task<PopugTask> ClosePopugTask(Guid actorId, Guid taskId)
        {
            var task = await _tasksRepository.GetById(taskId);
            
            if (task == null)
                throw new ArgumentException("Invalid taskId");

            var actor = await _usersRepository.GetById(actorId);

            if (actor == null)
                throw new ArgumentException("Invalid actorId");

            task.Close(actor);

            var closedTask =  await _tasksRepository.AddOrUpdate(task);
            await _businessEventsStream.StreamAboutTaskClosed(closedTask);
            return closedTask;
        }

        public async Task<PopugTask> GetById(Guid taskId)
        {
            var task = await _tasksRepository.GetById(taskId);

            return task;
        }
    }
}