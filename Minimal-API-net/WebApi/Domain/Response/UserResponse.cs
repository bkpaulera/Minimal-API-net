namespace WebApi.Domain.Response
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public string? Password { get; set; }
        public required string Email { get; set; }
        public string? Image { get; set; }

        public string[]? Roles;
        public string? Message { get; set; }
    }

}
