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

        public ITable<PopugTaskDb> PopugTasks => GetTable<PopugTaskDb>();
        public ITable<User> Users => GetTable<User>();
    }
}