using Microsoft.AspNetCore.Mvc;
using SignupApiCore.Models;
using SignupApiCore.Repositories;
using SignupApiCore.Routing;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SignupApiCore.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _repository.GetUsers();
        }

        [HttpGet("{userName}", Name = "GetUserByUsername")]
        public IActionResult Get(string userName)
        {
            var user = _repository.GetUserByUsername(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult Get([RequiredFromQuery] string userName, [RequiredFromQuery] string password)
        {
            var user = _repository.GetUserByUsername(userName, password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _repository.GetUserByUsername(user.UserName);
            if (existingUser != null)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            user = _repository.AddUser(user);            
            return CreatedAtRoute("GetUserByUsername", new { userName = user.UserName }, user);
        }
    }
}