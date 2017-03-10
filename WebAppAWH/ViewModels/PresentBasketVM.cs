using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAWH.Domain.Mapping;

namespace WebAppAWH.ViewModels
{
    public class PresentBasketVM : IMapFrom<PresentBasket>
    {
        public PresentBasket PresentBasket;
    }
}