using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using DataLayer.Repo;
using System.Data.Entity;
using Data.Repo.Users;

namespace Service.Parents
{
    public class ParentService : IParentService, IDisposable
    {
        private ApplicationDbContext context { get; set; }
        private readonly IParentRepository parentrepo;
        private UserManager<ApplicationUser> UserManager { get; set; }
        private ICurrentUser currentuser { get; set; }
        private IUnitOfWork unitofwork { get; set; }
        private IUserRepository userrepository { get; set; }

        public ParentService()
        {

        }

        public ParentService(IParentRepository _pr, IUnitOfWork _unitofwork)
        {
            parentrepo = _pr;
            unitofwork = _unitofwork;
        }

        public ParentService(ICurrentUser _user, IParentRepository _pr, ApplicationDbContext _context, IUnitOfWork _unitofwork, IUserRepository _userrepository)
        {
            currentuser = _user;
            parentrepo = _pr;
            context = _context;
            unitofwork = _unitofwork;
            userrepository = _userrepository;

            if (parentrepo == null)
            {
                throw new ApplicationException("No Parent Repository");
            }
        }

        public IEnumerable<Parent> GetAll()
        {
            return parentrepo.GetAll();
        }

        public void CreateParent(Parent parent)
        {
            if (currentuser.GetCurrentUser() != null)
            {
                parent.Creator = currentuser.GetCurrentUser();
                parentrepo.Insert(parent);
                unitofwork.Commit();
            }
        }

        public Parent GetParent(int id)
        {
            return parentrepo.GetById(id);
        }

        public void DeleteParent(int id)
        {
            Parent parent = GetParent(id);
            parentrepo.Delete(GetParent(id));
            unitofwork.Commit();
        }

        public void Delete(Parent parent)
        {
            parentrepo.Delete(parent);
            unitofwork.Commit();

        }
        public void EditParent(Parent parent)
        {
            parentrepo.Update(parent);
            unitofwork.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((disposing) & (context != null))
            {
                context.Dispose();
            }
        }
    }
}
