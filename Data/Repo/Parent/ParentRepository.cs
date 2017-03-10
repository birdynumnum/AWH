using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repo
{
    public class ParentRepository :  GenericRepository<Parent>, IParentRepository
    {

        public ParentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override void Insert(Parent entity)
        {
            base.Insert(entity);
        }

        public override Parent GetById(int id)
        {
            return base.GetById(id);
        }

        public override void Delete(Parent entity)
        {
            base.Delete(entity);
        }

        public Parent GetSingleParent(int Id)
        {
            var result = GetAll().FirstOrDefault(x => x.Id == Id);
            return result;
        }
    }
}
