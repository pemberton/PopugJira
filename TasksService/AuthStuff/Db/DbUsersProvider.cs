using Microsoft.Extensions.Configuration;

namespace AuthStuff.Db
{
    public class DbUsersProvider : IDbUsersProvider
    {
        private readonly IConfiguration _configuration;

        public DbUsersProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbUsers GetDbUsers()
        {
            return new DbUsers(new DbSettings
            {
                ConnectionString = _configuration.GetConnectionString("db_users")
            });
        }
    }
}