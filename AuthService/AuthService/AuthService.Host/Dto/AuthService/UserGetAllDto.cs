﻿namespace AuthService.Host.Dto
{
    public class UserGetAllDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
    }
}