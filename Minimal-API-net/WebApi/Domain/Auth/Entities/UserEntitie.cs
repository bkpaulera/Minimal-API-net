using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Auth.Models
{
    public record UserEntitie(
        Guid Id,
        string Username,
        string Password ,
        string Image,
        string Email 
    );
}
