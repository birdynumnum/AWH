using Providers.MailProvider;
using Service.Presents;
using Service.Parents;
using Service.Kids;
using Service;
using Service.Mailservice;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWH.Infrastructure
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IParentService>().Use<ParentService>();
            For<IPresentService>().Use<PresentService>();
            For<IUserService>().Use<UserService>();
            For<IKidService>().Use<KidService>();
            For<IMailer>().Use<SMTPService>();
        }
    }
}