using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Streams.Contracts;
using AuthService.Streams.Dto;

namespace AuthService.Streams
{
    public class AuthBusinessEventsStream : IUserBusinessEventsStream
    {
        private readonly IMessageBus _messageBus;

        public AuthBusinessEventsStream(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task UserWasCreated(ApplicationUser user)
        {
            var userDto = new UserCreatedStreamDto
            {
                Id = user.Id,
                UserName = user.UserName
            };

            await _messageBus.SendMessage(MessageBus.UsersTopic, userDto);
        }

        public async Task RoleWasCreated(string id, string name)
        {
            var roleCreatedDto = new RoleCreatedStreamDto
            {
                Id = id,
                Name = name
            };

            await _messageBus.SendMessage(MessageBus.RolesTopic, roleCreatedDto);
        }

        public async Task UserWasGrantedToRole(string userId, string roleName)
        {
            var roleGrantedDto = new UserWasGrantedToRoleStreamDto
            {
                UserId = userId,
                RoleName = roleName
            };

            await _messageBus.SendMessage(MessageBus.UserGrantsRolesTopic, roleGrantedDto);
        }
    }
}