using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SignupApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignupApiCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<User> GetUsers()
        {            
            return GetUsers(string.Empty);
        }

        public User GetUserByUsername(string userName)
        {
            var parameters = new List<(string, string)>
            {
                ("username", userName)
            };
            var users = GetUsers($"WHERE userName = ?username", parameters);
            return users.FirstOrDefault();
        }

        public User GetUserByUsername(string userName, string password)
        {
            var parameters = new List<(string, string)>
            {
                ("username", userName),
                ("password", password)
            };
            var users = GetUsers($"WHERE userName = ?username AND password = ?password", parameters);
            return users.FirstOrDefault();
        }

        public User AddUser(User user)
        {
            var sqlCommand = "INSERT INTO user (dateOfBirth, emailAddress, firstName, lastName, password, userName)";
            sqlCommand += "VALUES(?dateOfBirth, ?emailAddress, ?firstName, ?lastName, ?password, u?serName)";

            var parameters = new List<(string, object)>
            {
                ("dateOfBirth", user.DateOfBirth),
                ("emailAddress", user.EmailAddress),
                ("firstName", user.FirstName),
                ("lastName", user.LastName),
                ("password", user.Password),
                ("userName", user.UserName)
            };

            using (var connection = new MySqlConnection(_configuration["Database:ConnectionString"]))
            {
                connection.Open();
                using (var command = new MySqlCommand(sqlCommand, connection))
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                    }
                    command.ExecuteNonQuery();
                }
            }

            return GetUserByUsername(user.UserName, user.Password);            
        }

        private IEnumerable<User> GetUsers(string whereClause, IEnumerable<(string, string)> parameters = null)
        {
            var users = new List<User>();
            using (var connection = new MySqlConnection(_configuration["Database:ConnectionString"]))
            {
                connection.Open();
                using (var command = new MySqlCommand($"SELECT * FROM user {whereClause}", connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Item1, parameter.Item2);
                        }
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(MapUser(reader));
                        }
                    }
                }
            }
            return users;
        }

        private static User MapUser(MySqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt64("id"),
                UserName = reader.GetString("userName"),
                Password = reader.GetString("password"),
                FirstName = reader.GetString("firstName"),
                LastName = reader.GetString("lastName"),
                EmailAddress = reader.GetString("emailAddress"),
                DateOfBirth = reader.GetDateTime("dateOfBirth")
            };
        }
    }
}
