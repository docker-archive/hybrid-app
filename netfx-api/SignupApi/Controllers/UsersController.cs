using SignupApi.Entities;
using SignupApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignupApi.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (var context = new SignUpContext())
            {
                return context.Users.ToList();
            }                
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult Get(string id)
        {
            using (var context = new SignUpContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult Get(string userName, string password)
        {
            using (var context = new SignUpContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
        }

        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostAuthor(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var context = new SignUpContext())
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            
            return CreatedAtRoute("DefaultApi", new { userName = user.UserName }, user);
        }
    }
}