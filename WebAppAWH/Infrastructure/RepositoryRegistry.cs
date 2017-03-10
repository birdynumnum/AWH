using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using DataLayer.Repo;
using Domain;
using StructureMap;
using Data.Repo.Presents;
using Data.Repo.Kids;
using Data.Repo.Users;
using Service.Parents;

namespace WebAppAWH.Infrastructure
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IParentRepository>().Use<ParentRepository>();
            For<IPresentRepository>().Use<PresentRepository>();
            For<IKidRepository>().Use<KidRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IUnitOfWork>().Use<UnitOfWork>();
        }
    }
}