using System.Threading.Tasks;
using AuthStuff.StreamEventsDto;

namespace AuthStuff.Consumers
{
    public interface IUserRoleConsumer:IConsumer
    {
    }

    public class UserRoleConsumer : Consumer<UserWasGrantedToRole>, IUserRoleConsumer
    {
        private readonly IUsersRepository _usersRepository;

        public UserRoleConsumer(IUsersRepository usersRepository)
            : base("group_users")
        {
            _usersRepository = usersRepository;
            TopicName = "users_roles_topic";
        }


        protected override async Task HandleMessage(UserWasGrantedToRole message)
        {
            await _usersRepository.AddRoleToUser(message.UserId, message.RoleName);
        }
    }
}