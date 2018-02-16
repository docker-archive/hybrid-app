using System.Collections.Generic;
using SignupApiCore.Models;

namespace SignupApiCore.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUserByUsername(string userName);

        User GetUserByUsername(string userName, string password);

        User AddUser(User user);
    }
}