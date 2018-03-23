using NLog;
using SignupApi.Entities;
using SignupApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignupApi.Controllers
{
    public class UsersController : ApiController
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();

        public IHttpActionResult Get()
        {
            _Logger.Debug("Fetch all users started");
            try
            {
                using (var context = new SignUpContext())
                {
                    var users = context.Users.ToList();
                    _Logger.Debug("Fetch all users completed");
                    return Ok(users);
                }                
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, "Fetch all users failed");
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult Get(string id)
        {
            _Logger.Debug("Fetch user by ID started: {0}", id);
            try
            {
                using (var context = new SignUpContext())
                {
                    var user = context.Users.FirstOrDefault(x => x.UserName == id);
                    if (user == null)
                    {
                        _Logger.Info("Fetch user by ID not found: {0}", id);
                        return NotFound();
                    }
                    _Logger.Debug("Fetch user by ID completed: {0}", id);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, "Fetch user by ID failed: {0}", id);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult Get(string userName, string password)
        {
            _Logger.Debug("Fetch user by username started: {0}", userName);
            try
            {
                using (var context = new SignUpContext())
                {
                    var user = context.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
                    if (user == null)
                    {
                        _Logger.Info("Fetch user by username not found: {0}", userName);
                        return NotFound();
                    }
                    _Logger.Debug("Fetch user by username completed: {0}", userName);
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, "Fetch user by username failed: {0}", userName);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _Logger.Debug("Create user started: {0}", user.UserName);
            try
            {
                using (var context = new SignUpContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }
                _Logger.Debug("Create user completed: {0}", user.UserName);
                return CreatedAtRoute("DefaultApi", new { userName = user.UserName }, user);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, "Create user failed: {0}", user.UserName);
                return InternalServerError(ex);
            }
        }
    }
}