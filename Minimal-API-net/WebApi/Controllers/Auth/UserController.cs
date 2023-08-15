using Domain.Auth.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUsersRepository _usersRepository;
        public UserController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // GET: api/<UuserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _usersRepository.GetAll();

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
        public void Post([FromBody] string value)
        {
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
