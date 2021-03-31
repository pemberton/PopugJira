using System.Threading.Tasks;

namespace AuthService.Streams.Contracts
{
    public interface IMessageBus
    {
        Task SendMessage<T>(string topic, T message);
    }
}