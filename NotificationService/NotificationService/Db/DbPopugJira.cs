using LinqToDB.Data;

namespace NotificationService.Db
{
    public class DbPopugJira : DataConnection
    {
        public DbPopugJira(DbSettings dbSettings)
            : base(dbSettings.DataProvider, dbSettings.ConnectionString)
        {
        }
    }
}