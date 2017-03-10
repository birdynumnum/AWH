using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PresentBasket
    {
        public int Id { get; set; }
        public List<PresentBasketLine> BasketItems { get; set; }

        public PresentBasket()
        {
            BasketItems = new List<PresentBasketLine>();
        }

        public void AddPresent(Present present)
        {
            //TODO: set isselected to true 
            //TODO Save presentbasket to current user
            if (present != null)
            {
                BasketItems.Add(new PresentBasketLine (present, 1));
            }
        }

        public void RemovePresent(Present present)
        {
            BasketItems.RemoveAll(l => l.Present.Id == present.Id);

        }

        public void ClearAll()
        {
            BasketItems.Clear();
        }

        public decimal ComputeTotalPresentValue()
        {
           // return BasketItems.Sum(e => e.Present.Quantity * e.Present.Quantity);
            decimal total = 0.0m;

            foreach(PresentBasketLine p in BasketItems)
            {
                total = total + (p.Present.Price * p.Present.Quantity);
            }
            return total;
        }
    }
}
