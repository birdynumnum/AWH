using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppAWH.Domain.Mapping;

namespace WebAppAWH.ViewModels
{
    public class PresentVM : IMapFrom<Present>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        
        [DataType(DataType.Url)]
        public string URL { get; set; }

        public string Brand { get; set; }
        public decimal Price { get; set; }
        public Int16 Quantity { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}