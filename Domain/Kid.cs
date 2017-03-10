using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Kid : IObjectWithState
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public virtual Parent parent { get; set; }
        public ApplicationUser Creator { get; set; }

        public Kid()
        {

        }

        public Kid(string FN, string LN, DateTime dob)
        {
            FirstName = FN;
            LastName = LN;
            DOB = dob;
        }

        [NotMapped]
        public State State { get; set; }
    }
}
