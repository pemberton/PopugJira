namespace AuthService.Streams.Dto
{
    public class UserWasGrantedToRoleStreamDto
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}