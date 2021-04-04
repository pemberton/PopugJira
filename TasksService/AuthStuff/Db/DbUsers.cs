using LinqToDB;
using LinqToDB.Data;

namespace AuthStuff.Db
{
    public class DbUsers : DataConnection
    {
        public DbUsers(DbSettings dbSettings)
            : base(dbSettings.DataProvider, dbSettings.ConnectionString)
        {
        }

        public ITable<User> Users => GetTable<User>();
    }
}