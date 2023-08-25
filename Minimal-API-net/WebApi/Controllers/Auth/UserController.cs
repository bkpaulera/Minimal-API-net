using Domain.Auth;
using Domain.Auth.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.Auth;
using WebApi.Domain.Auth.Interfaces.Service;
using WebApi.Domain.Request;
using WebApi.Domain.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UuserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAll();

            return Ok(result);
        }

        // GET api/<UuserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UuserController>
        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostAsync([FromBody] UserRequest request)
        {

            var result = await _userService.CreateUser(request);

            return Ok(result);
        }

        // PUT api/<UuserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UuserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
