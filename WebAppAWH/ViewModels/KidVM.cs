using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppAWH.Domain.Mapping;

namespace WebAppAWH.ViewModels
{
    public class KidVM : IMapFrom<Kid>
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Expected date of birth")]
        public DateTime DOB { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}