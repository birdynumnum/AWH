using DataLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Linq.Expressions;

namespace Data.Repo.Kids
{
    public class KidRepository : GenericRepository<Kid>, IKidRepository
    {
        public KidRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override IEnumerable<Kid> GetAll()
        {
            return base.GetAll();
        }

        public override Kid GetById(int id)
        {
            return base.GetById(id);
        }

        public override void Insert(Kid entity)
        {
            base.Insert(entity);
        }

        public override void Delete(Kid entity)
        {
            base.Delete(entity);
        }

        public override IEnumerable<Kid> SearchFor(System.Linq.Expressions.Expression<Func<Kid, bool>> predicate)
        {
            return base.SearchFor(predicate);
        }
    }
}
