using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;
        
        public ApplicationDbContext Context 
        {
            get { return context; }
        }

        public UnitOfWork(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}
