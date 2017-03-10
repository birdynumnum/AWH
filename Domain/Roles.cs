using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum Roles : int
    {
        Father = 0, 
        Mother = 1, 

        [Display(Name = "Grand Father")]
        Grandfather = 2,

        [Display (Name= "Grand Mother")]
        GrandMother = 3,

        [Display(Name = "Friend of the family")]
        Friendofthefamily = 4
    }
}
