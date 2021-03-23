using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Mapster;
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

            var recordQuery = db.PopugTasks
                .LoadWith(g => g.Creator)
                .LoadWith(g => g.Assignee);

            var record = await recordQuery
                .SingleOrDefaultAsync(s => s.Id == popugTaskId);

            return record.Adapt<PopugTask>();
        }

        public async Task<List<PopugTask>> GetByAssignee(Guid assigneeId)
        {
            await using var db = _dbProvider.GetDbPopugJira();
            
            var records = await db.PopugTasks
                .LoadWith(g => g.Creator)
                .LoadWith(g => g.Assignee)
                .Where(s => s.AssigneeId == assigneeId)
                .LoadWith(g => g.Creator)
                .ToListAsync();

            return records
                .Select(r => r.Adapt<PopugTask>())
                .ToList();;
        }

        public async Task<PopugTask> AddOrUpdate(PopugTask newTask)
        {
            await using var db = _dbProvider.GetDbPopugJira();

            if (newTask.Id == Guid.Empty)
                newTask.Id = Guid.NewGuid();

            var dbEntity = newTask.Adapt<PopugTaskDb>();
            
            await db.InsertOrReplaceAsync(dbEntity);

            return newTask;
        }

        public async Task<List<PopugTask>> GetAll()
        {
            await using var db = _dbProvider.GetDbPopugJira();

            var records = await db.PopugTasks
                .LoadWith(g => g.Creator)
                .LoadWith(g => g.Assignee)
                .ToListAsync();

            return records
                .Select(r => r.Adapt<PopugTask>())
                .ToList();
        }
    }
}