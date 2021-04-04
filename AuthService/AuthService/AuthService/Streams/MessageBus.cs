using System.Threading.Tasks;
using AuthService.Streams.Contracts;
using Confluent.Kafka;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AuthService.Streams
{
    public class MessageBus : IMessageBus
    {
        private readonly ProducerConfig config;
        public const string UsersTopic = "users_topic";
        public const string RolesTopic = "roles_topic";
        public const string UserGrantsRolesTopic = "users_roles_topic";

        public MessageBus()
        {
            config = new ProducerConfig
                { BootstrapServers = "localhost:9092" };;
        }

        public async Task SendMessage<T>(string topic, T message)
        {
            var messageString = JsonSerializer.Serialize(message);
            using var producer = new ProducerBuilder<Null, string>(config)
                .Build();
            await producer.ProduceAsync(topic, new Message<Null, string> {Value = messageString});
        }
    }
}