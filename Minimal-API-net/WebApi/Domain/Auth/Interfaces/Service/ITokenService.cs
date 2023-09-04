using System.Security.Claims;
using WebApi.Domain.Auth.Models;

namespace WebApi.Domain.Auth.Interfaces.Service
{
    public interface ITokenService
    {
        public string Generate(Users user);
    }
}
