using LinqToDB;
using LinqToDB.Data;
using TasksService.BO;

namespace TasksService.Db
{
    public class DbPopugJira : DataConnection
    {
        public DbPopugJira(DbSettings dbSettings)
            : base(dbSettings.DataProvider, dbSettings.ConnectionString)
        {
        }

        public ITable<PopugTask> PopugTasks => GetTable<PopugTask>();
        public ITable<User> Users => GetTable<User>();
    }
}