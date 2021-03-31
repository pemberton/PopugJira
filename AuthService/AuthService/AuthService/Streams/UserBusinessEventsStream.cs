using System;
using System.Threading.Tasks;
using AuthService.BO;
using AuthService.Streams.Contracts;
using AuthService.Streams.Dto;

namespace AuthService.Streams
{
    public class UserBusinessEventsStream : IUserBusinessEventsStream
    {
        private readonly IMessageBus _messageBus;

        public UserBusinessEventsStream(IMessageBus messageBus)
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
    }
}