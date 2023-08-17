using System.Globalization;

namespace Domain.Auth
{
    public class Users
    {
        public Users(Guid id , string username , string password) { 
            Id = id;
            Username = username;
            Password = password;
        }

        public Guid Id { get; set; }
        public  string Username { get; set; }

        public string Password { get; set; }
    }
}