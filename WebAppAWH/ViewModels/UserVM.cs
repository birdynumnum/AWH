using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppAWH.Domain.Mapping;

namespace WebAppAWH.ViewModels
{
    public class UserVM : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}