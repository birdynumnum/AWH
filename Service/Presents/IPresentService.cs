using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Presents
{
    public interface IPresentService
    {
        IEnumerable<Present> GetAll();
        Present GetPresent(int id);
        void CreatePresent(Present present);
        void DeletePresent(Present present);
        void EditPresent(Present present);
    }
}
