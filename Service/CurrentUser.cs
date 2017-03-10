using Domain;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace Service
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ApplicationDbContext context;
        private ApplicationUser _user { get; set; }
        private readonly IIdentity identity;

        public CurrentUser(ApplicationDbContext _context, IIdentity _identity)
        {
            context = _context;
            identity = _identity;
        }

        public ApplicationUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        public ApplicationUser GetCurrentUser()
        {
            return User = context.Users.Find(identity.GetUserId());
        }
    }
}