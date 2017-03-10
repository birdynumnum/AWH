using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Kids
{
    public interface IKidService
    {
        IEnumerable<Kid> GetAllKids();
        void CreateKid(Kid kid);
        Kid GetKid(int id);
        void DeleteKid(int id);
        void EditKid(Kid kid);
    }
}
