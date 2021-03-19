using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TasksService.BO;
using TasksService.Db;
using TasksService.Repositories.Contracts;

namespace TasksService.Repositories
{
    public class PopugTaskRepository : IPopugTaskRepository
    {
        private readonly IDbProvider _dbProvider;

        public PopugTaskRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider ?? throw new ArgumentNullException(nameof(dbProvider));
        }
        
        public async Task<PopugTask> GetById(Guid popugTaskId)
        {
            await using var db = _dbProvider.GetDbPopugJira();
            
            var record = await db.PopugTasks
                .SingleOrDefaultAsync(s => s.Id == popugTaskId);

            return record;
        }

        public async Task<List<PopugTask>> GetByAssignee(Guid assigneeId)
        {
            await using var db = _dbProvider.GetDbPopugJira();
            
            var records = await db.PopugTasks
                .Where(s => s.Assignee.Id == assigneeId)
                .ToListAsync();

            return records;
        }

        public async Task<PopugTask> AddOrUpdate(PopugTask newTask)
        {
            await using var db = _dbProvider.GetDbPopugJira();

            if (newTask.Id == Guid.Empty)
                newTask.Id = Guid.NewGuid();
            
            await db.InsertOrReplaceAsync(newTask);

            return newTask;
        }
    }
}