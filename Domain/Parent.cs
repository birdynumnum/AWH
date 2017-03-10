using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Parent : IObjectWithState
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }

        public ApplicationUser Creator { get; set; } 
        public ICollection<Kid> Kids { get; set; }
        public ICollection<Present> Presents { get; set; }

        public Parent()
        {

        }

        public Parent(string FN, string LN, string email, Roles r)
        {
            FirstName = FN;
            LastName = LN;
            Email = email;
            Role = r;
        }

        [NotMapped]
        public State State { get; set; }
    }
}
