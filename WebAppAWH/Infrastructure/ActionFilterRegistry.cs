using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.TypeRules;

namespace WebAppAWH.Infrastructure
{
    public class ActionFilterRegistry : Registry
    {
        public ActionFilterRegistry(Func<IContainer> containerFactory)
        {
            {
                For<IFilterProvider>().Use(
                    new StructureMapFilterProvider(containerFactory));

                Policies.SetAllProperties(x =>
                    x.Matching(p =>
                        p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
                        p.DeclaringType.Namespace.StartsWith("WebAppAWH") &&
                        !p.PropertyType.IsPrimitive &&
                        p.PropertyType != typeof(string)));
            }
        }
    }
}