using DataLayer.Repo;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Users
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IEnumerable<ApplicationUser> GetAll()
        {
            return base.GetAll();
        }

        public override void Insert(ApplicationUser entity)
        {
            base.Insert(entity);
        }

        public override void Delete(ApplicationUser entity)
        {
            base.Delete(entity);
        }

        public override IEnumerable<ApplicationUser> SearchFor(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return base.SearchFor(predicate);
        }

        public override IQueryable<ApplicationUser> AllIncluding(params Expression<Func<ApplicationUser, object>>[] includeProperties)
        {
            return base.AllIncluding(includeProperties);
        }
    }
}
