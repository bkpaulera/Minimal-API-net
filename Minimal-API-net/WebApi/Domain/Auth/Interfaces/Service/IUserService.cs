using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Request;
using WebApi.Domain.Response;

namespace WebApi.Domain.Auth.Interfaces.Service
{
    public interface IUserService
    {
        Task<UserResponse> CreateUser(UserRequest request);
        Task<ActionResult<IEnumerable<UserResponse>>> GetAll();
    }
}
