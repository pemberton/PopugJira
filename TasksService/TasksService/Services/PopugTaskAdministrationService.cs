using System;
using System.Collections.Generic;
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

        public Task<List<PopugTask>> GetByAssignee(Guid assigneeId)
        {
            return _tasksRepository.GetByAssignee(assigneeId);
        }

        public async Task<PopugTask> CreateNew(PopugTask newTask)
        {
            newTask.TaskState = TaskState.Open;
            newTask.Created = DateTime.Now;
            
            // TODO: creator
            var admin = await _usersRepository.GetById(Guid.Parse("EB6F4CA7-17B8-43CB-8D61-784B4BF55D6C"));
            newTask.Creator = admin;
            
            newTask.Validate();
            return await _tasksRepository.AddOrUpdate(newTask);
        }

        public async Task<PopugTask> AssignToUser(Guid taskId, Guid userId)
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
        
        public async Task<PopugTask> ClosePopugTask(Guid taskId)
        {
            var task = await _tasksRepository.GetById(taskId);
            
            if (task == null)
                throw new ArgumentException("Invalid taskId");
            
            task.ClosedAt = DateTime.Now;
            task.TaskState = TaskState.Done;
            return  await _tasksRepository.AddOrUpdate(task);
        }
        
        public async Task<PopugTask> ReopenPopugTask(Guid taskId)
        {
            var task = await _tasksRepository.GetById(taskId);
            
            if (task == null)
                throw new ArgumentException("Invalid taskId");
            
            task.ClosedAt = null;
            task.TaskState = TaskState.Open;
            return  await _tasksRepository.AddOrUpdate(task);
        }
    }
}