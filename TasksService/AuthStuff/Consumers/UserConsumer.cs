using System.Threading.Tasks;
using AuthStuff.StreamEventsDto;

namespace AuthStuff.Consumers
{
    public interface IUserConsumer : IConsumer
    {
    }

    public class UserConsumer : Consumer<UserWasCreated>, IUserConsumer
    {
        private readonly IUsersRepository _usersRepository;

        public UserConsumer(IUsersRepository usersRepository)
            : base("group_users")
        {
            _usersRepository = usersRepository;
            TopicName = "users_topic";
        }

        protected override async Task HandleMessage(UserWasCreated message)
        {
            var user = new User
            {
                Id = message.Id,
                Name = message.UserName
            };

            await _usersRepository.AddUser(user);
        }
    }
}