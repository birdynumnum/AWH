using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
        ApplicationUser GetCurrentUser();

    }
}
