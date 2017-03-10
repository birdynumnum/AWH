using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Parents
{
    public interface IParentService
    {
        IEnumerable<Parent> GetAll();
        Parent GetParent(int id);
        void CreateParent(Parent parent);
        void EditParent(Parent parent);
        void Delete(Parent parent);
    }
}
