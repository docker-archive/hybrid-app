using MySql.Data.Entity;
using SignupApi.Models;
using System.Data.Entity;

namespace SignupApi.Entities
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SignUpContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}