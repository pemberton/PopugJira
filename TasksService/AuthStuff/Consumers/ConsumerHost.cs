using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AuthStuff.Consumers
{
    public class ConsumerHost : IHostedService
    {
        private readonly IUserConsumer _usersConsumer;
        private readonly IUserRoleConsumer _usersRolesConsumer;

        public ConsumerHost(
            IUserConsumer usersConsumer,
            IUserRoleConsumer usersRolesConsumer)
        {
            _usersConsumer = usersConsumer;
            _usersRolesConsumer = usersRolesConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Task.Run(() => _usersConsumer.StartAsync(cancellationToken), cancellationToken);
                Task.Run(() => _usersRolesConsumer.StartAsync(cancellationToken), cancellationToken);

            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _usersConsumer.StopAsync(cancellationToken);
            _usersRolesConsumer.StopAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}