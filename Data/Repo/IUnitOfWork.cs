using System;

namespace DataLayer.Repo
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
