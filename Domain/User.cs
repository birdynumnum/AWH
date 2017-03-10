using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : IObjectWithState
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        [NotMapped]
        public State State { get; set; }
    }
}
