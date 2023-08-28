using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Request;
using WebApi.Domain.Response;

namespace WebApi.Domain.Auth.Interfaces.Service
{
    public interface IUserService
    {
        Task<UserResponse> CreateUser(UserRequest request);
        Task<ActionResult<IEnumerable<UserResponse>>> GetAll();
        Task<ActionResult<UserResponse>> GetUser(UserRequest request);
        Task<ActionResult<UserResponse>> PutUserPassword(string id ,string newpassword ,UserRequest request);
        Task<ActionResult<UserResponse>> DeleteUser(string id, UserRequest request);
    }
}
