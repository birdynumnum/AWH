using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppAWH.Domain.Mapping;

namespace WebAppAWH.ViewModels
{
    public class ParentVM : IMapFrom<Parent>
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public Roles Role { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}