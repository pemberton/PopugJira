using System;

namespace AuthService.Host.Dto
{
    public class UserGetAllDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}