using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace AuthStuff.Consumers
{
    public interface IConsumer
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }

    public abstract class Consumer<T> : IConsumer
    {
        private readonly ConsumerConfig conf;

        protected Consumer(string groupId)
        {
            conf = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        protected string TopicName;

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

                    var message = JsonSerializer.Deserialize<T>(consumer.Message.Value, options);
                    await HandleMessage(message);
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

        protected abstract Task HandleMessage(T message);
    }
}