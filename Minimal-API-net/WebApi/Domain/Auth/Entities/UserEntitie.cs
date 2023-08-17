using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Auth.Models
{
    public class UserEntitie
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
