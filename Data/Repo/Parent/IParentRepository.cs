using Domain;
using System;
using System.Threading.Tasks;

namespace DataLayer.Repo
{
    public interface IParentRepository : IGenericRepository<Parent>
    {
        Parent GetSingleParent(int Id);
    }
}
