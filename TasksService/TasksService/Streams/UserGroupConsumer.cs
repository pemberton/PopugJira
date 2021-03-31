using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using TasksService.BO;
using TasksService.Repositories.Contracts;

namespace TasksService.Streams
{
    public class HostedConsumer : IHostedService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ConsumerConfig conf;

        public HostedConsumer(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            conf = new ConsumerConfig
            {
                GroupId = GroupId,
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public string GroupId = "users_group";
        public string TopicName = "users_topic";

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var builder = new ConsumerBuilder<Ignore, string>(conf)
                .Build();

            builder.Subscribe(TopicName);

            var cancelToken = new CancellationTokenSource();

            try
            {
                while (true)
                {
                    var consumer = builder.Consume(cancelToken.Token);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var user = JsonSerializer.Deserialize<User>(consumer.Message.Value, options);
                    await _usersRepository.AddOrUpdate(user);
                }
            }
            catch (Exception)
            {
                builder.Close();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}