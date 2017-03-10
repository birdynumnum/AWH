using Data.Repo.Users;
using DataLayer.Repo;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Providers.MailProvider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Service
{
    public class UserService : IUserService, IDisposable
    {
        private ApplicationDbContext context { get; set; }
        private IUserRepository userrepository { get; set; }
        private IMailer mailer { get; set; }
        private IUnitOfWork unitofwork { get; set; }
        private ICurrentUser currentuser { get; set; }

        public UserService(ApplicationDbContext _context, IUserRepository _userrepository, IMailer _mailer, IUnitOfWork _unitofwork, ICurrentUser _currentuser)
        {
            context = _context;
            userrepository = _userrepository;
            mailer = _mailer;
            unitofwork = _unitofwork;
            currentuser = _currentuser;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return context.Users;
        }

        public ApplicationUser GetUser(string id)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            ApplicationUser founduser = userrepository.SearchFor(e => e.Id == id).FirstOrDefault();
            return founduser;
        }

        public void CreateUser(User user)
        {
            //This should be extracted away... I really, really dont like the look of this...
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //...

            var invitee = new ApplicationUser { UserName = user.Username, Email = user.Email };

            //Check if user allready exists
            var existinguser = userrepository.SearchFor(e => e.Email == invitee.Email).FirstOrDefault();
            if (existinguser == null)
            {
                invitee.Creator = currentuser.GetCurrentUser();

                user.Password = Membership.GeneratePassword(8, 2);
                userManager.Create(invitee, user.Password);
                userManager.AddToRole(invitee.Id, "invitee");
                context.Users.Add(invitee);

                mailer.SendMail(currentuser.GetCurrentUser().Email, invitee.Email, "Hi there", user.Username,  user.Password);
            }
            else
            {
                throw new Exception("User allready exists");
            }
        }

        public void DeleteUser(ApplicationUser user)
        {
            try
            {
                userrepository.Delete(user);
                unitofwork.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.ToString());
            }
        }

        public void UpdateUser(ApplicationUser user)
        {
            userrepository.Update(user);
            unitofwork.Commit();
        }

        public IEnumerable<ApplicationUser> GetAllInclude()
        {
            return userrepository.AllIncluding(c => c.Creator);
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
