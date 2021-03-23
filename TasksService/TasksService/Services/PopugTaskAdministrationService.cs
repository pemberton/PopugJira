using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TasksService.BO;
using TasksService.Repositories.Contracts;
using TasksService.Services.Contracts;

namespace TasksService.Services
{
    public class PopugTaskAdministrationService : IPopugTaskAdministrationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPopugTaskRepository _tasksRepository;

        public PopugTaskAdministrationService(
            IUsersRepository usersRepository,
            IPopugTaskRepository tasksRepository)
        {
            _usersRepository = usersRepository;
            _tasksRepository = tasksRepository;
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
            newTask.TaskState = TaskState.Open;
            newTask.Created = DateTime.Now;
            
            newTask.Creator = await _usersRepository.GetById(creatorId);
            
            newTask.Validate();
            return await _tasksRepository.AddOrUpdate(newTask);
        }

        public async Task<PopugTask> AssignToUser(Guid actorId, Guid taskId, Guid userId)
        {
            var task = await _tasksRepository.GetById(taskId);
            
            if (task == null)
                throw new ArgumentException("Invalid taskId");

            var user = await _usersRepository.GetById(userId);
            
            if (user == null)
                throw new ArgumentException("Invalid userId");
            
            task.Assignee = user;
            return  await _tasksRepository.AddOrUpdate(task);
        }
        
        public async Task<PopugTask> ClosePopugTask(Guid actorId, Guid taskId)
        {
            var task = await _tasksRepository.GetById(taskId);
            
            if (task == null)
                throw new ArgumentException("Invalid taskId");
            
            task.ClosedAt = DateTime.Now;
            task.TaskState = TaskState.Done;
            return  await _tasksRepository.AddOrUpdate(task);
        }

        public async Task<PopugTask> GetById(Guid taskId)
        {
            var task = await _tasksRepository.GetById(taskId);

            return task;
        }
    }
}