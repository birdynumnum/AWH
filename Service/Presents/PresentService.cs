using Data.Repo.Presents;
using DataLayer.Repo;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Presents
{
    public class PresentService : IPresentService, IDisposable
    {
        private readonly IPresentRepository presentrepo;
        private IUnitOfWork unitofwork { get; set; }
        private ICurrentUser currentuser { get; set; }

        public PresentService(IPresentRepository _presentrepo, IUnitOfWork _unitofwork, ICurrentUser _currentuser)
        {
            presentrepo = _presentrepo;
            unitofwork = _unitofwork;
            currentuser = _currentuser;
        }

        public IEnumerable<Present> GetAll()
        {
            return presentrepo.GetAll().Where(i => i.IsSelected == false);
        }

        public Present GetPresent(int id)
        {
            return presentrepo.GetAll().Where(x => x.Id == id).First();
        }

        public void CreatePresent(Present present)
        {
            if (currentuser.GetCurrentUser() != null)
            {
                present.Creator = currentuser.GetCurrentUser();
                presentrepo.Insert(present);
                unitofwork.Commit();
            }
        }

        public void DeletePresent(Present present)
        {
            presentrepo.Delete(present);
            unitofwork.Commit();
        }

        public void EditPresent(Present present)
        {
            presentrepo.Update(present);
            unitofwork.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                this.Dispose();
            }
        }
    }
}
