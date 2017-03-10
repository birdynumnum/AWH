using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Present : IObjectWithState
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public bool IsSelected { get; set; }
        public Int16 Quantity { get; set; }
        public ApplicationUser Creator { get; set; }

        public Present()
        {

        }

        public Present(string name, string description, string url, string brand, decimal price, Int16 quantity, bool isselected)
        {

            Name = name;
            Description = description;
            URL = url;
            Brand = brand;
            Price = price;
            Quantity = quantity;
            IsSelected = isselected;
        }

        [NotMapped]
        public State State { get; set; }
        
    }
}
