using Data.Repo.Kids;
using DataLayer.Repo;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Kids
{
    public class KidService : IKidService
    {
        private IKidRepository kidrepo { get; set; }
        private IUnitOfWork unitofwork { get; set; }
        private ICurrentUser currentuser { get; set; }

        public KidService(IKidRepository _kidrepo, IUnitOfWork _unitofwork, ICurrentUser _currentuser)
        {
            kidrepo = _kidrepo;
            unitofwork = _unitofwork;
            currentuser = _currentuser;
        }

        public IEnumerable<Kid> GetAllKids()
        {
            return kidrepo.GetAll();
        }

        public Kid GetKid(int id)
        {
            Kid kid = kidrepo.GetById(id);
            return kid;
        }

        public void DeleteKid(int id)
        {
            Kid kid = kidrepo.GetById(id);
            kidrepo.Delete(kid);
            unitofwork.Commit();
        }

        public void CreateKid(Kid kid)
        {
            if (currentuser.GetCurrentUser() != null)
            {
                kid.Creator = currentuser.GetCurrentUser();
                kidrepo.Insert(kid);
                unitofwork.Commit();
            }
        }

        public void EditKid(Kid kid)
        {
            kidrepo.Update(kid);
            unitofwork.Commit();
        }
    }
}
