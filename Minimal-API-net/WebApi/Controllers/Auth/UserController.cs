using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Domain.Auth.Interfaces.Service;
using WebApi.Domain.Request;
using WebApi.Domain.Response;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {    
            try{

                ActionResult<IEnumerable<UserResponse>> result = await _userService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Detalhes: " + ex.Message);
            }

        }

        [HttpGet("get-user")]
        public async Task<ActionResult<UserResponse>> GetUser([FromBody] UserRequest request)
        {
            try
            {
                var result = await _userService.GetUser(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Detalhes: " + ex.Message);
            }
        }

        // POST api/<UuserController>
        [HttpPost("Post-User")]
        public async Task<ActionResult<UserResponse>> PostAsync([FromBody] UserRequest request)
        {
            try
            {
                var result = await _userService.CreateUser(request);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest("Detalhes: " + ex.Message);
            }
        }

        // PUT api/<UuserController>/5
        [HttpPut("Put-User")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResponse>> Put(string id, string newpassword, [FromBody] UserRequest request)
        {
            try
            {
                var result = await _userService.PutUserPassword(id,newpassword, request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest("Detalhes: " + ex.Message);
            }

        }

        // DELETE api/<UuserController>/5
        [HttpDelete("Delete-Uuser")]
        [ProducesResponseType(typeof(UserResponse), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<UserResponse>>  Delete(string id, [FromBody] UserRequest request)
        {
            try
            {
                var result = await _userService.DeleteUser(id, request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest("Detalhes: " + ex.Message);
            }

        }
    }
}
