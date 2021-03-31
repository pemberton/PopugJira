using Microsoft.Extensions.Configuration;

namespace NotificationService.Db
{
    public class DbProvider : IDbProvider
    {
        private readonly IConfiguration _configuration;

        public DbProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbPopugJira GetDbPopugJira()
        {
            return new DbPopugJira(new DbSettings
            {
                ConnectionString = _configuration.GetConnectionString("db_popugJira")
            });
        }
    }
}