namespace AuthService.Host.Dto.AuthService
{
    public class UserLoginDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }

        public string Role { get; set; }
    }
}