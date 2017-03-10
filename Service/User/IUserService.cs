using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser GetUser(string id);
        IEnumerable<ApplicationUser> GetAllInclude();
        void CreateUser(User user);
        void DeleteUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
    }
}
