using LinqToDB.Data;

namespace AccountingService.Db
{
    public class DbPopugJira : DataConnection
    {
        public DbPopugJira(DbSettings dbSettings)
            : base(dbSettings.DataProvider, dbSettings.ConnectionString)
        {
        }
    }
}