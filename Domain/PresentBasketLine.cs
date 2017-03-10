using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PresentBasketLine
    {
        public Present Present { get; set; }
        public int Quantity { get; set; }

        public PresentBasketLine(Present present, Int16 quantity)
        {
            Present = present;
            Present.Quantity = quantity;
        }
    }
}
