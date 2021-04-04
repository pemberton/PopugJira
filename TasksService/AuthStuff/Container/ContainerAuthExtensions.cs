using AuthStuff.Consumers;
using AuthStuff.Db;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AuthStuff.Container
{
    public static class ContainerAuthExtensions
    {
        public static void TryAddScopedAuthServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IDbUsersProvider, DbUsersProvider>();
            services.TryAddSingleton<IDbUsersProvider, DbUsersProvider>();
            services.TryAddSingleton<IUsersRepository, UsersRepository>();
            services.TryAddSingleton<IUserConsumer, UserConsumer>();
            services.TryAddSingleton<IUserRoleConsumer, UserRoleConsumer>();
        }

        public static void AddHostedAuthService(this IServiceCollection services)
        {
            services.AddHostedService<ConsumerHost>();

        }
    }
}