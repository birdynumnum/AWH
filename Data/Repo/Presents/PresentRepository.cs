using DataLayer.Repo;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Presents
{
    public class PresentRepository : GenericRepository<Present>, IPresentRepository
    {
        public PresentRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public override void Insert(Present entity)
        {
            base.Insert(entity);
        }

        public override void Update(Present entity)
        {
            base.Update(entity);
        }

        public override void Delete(Present entity)
        {
            base.Delete(entity);
        }
    }
}
