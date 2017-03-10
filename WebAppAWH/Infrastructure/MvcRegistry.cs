using AutoMapper;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Service;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAppAWH.Infrastructure
{
    public class MvcRegistry : Registry
    {
        public MvcRegistry()
        {
            For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
            For<DbContext>().Use<ApplicationDbContext>(new ApplicationDbContext());
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
            For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
            For<ICurrentUser>().Use<CurrentUser>();
        }
    }
}