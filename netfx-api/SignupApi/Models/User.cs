using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignupApi.Models
{
    [Table("user")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}