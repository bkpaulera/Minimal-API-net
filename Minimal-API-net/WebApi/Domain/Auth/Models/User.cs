using System.Globalization;

namespace WebApi.Domain.Auth.Models
{
    public class Users
    {
        public Users(Guid id, string username, string password, string image, string email, string[] roles)
        {
            Id = id;
            Username = username;
            Password = password;
            Image = image;
            Email = email;
            Roles = roles;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string[] Roles;

    }
}