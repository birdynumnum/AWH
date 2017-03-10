using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebAppAWH.Infrastructure
{
    public class StructureMapDependancyResolver : IDependencyResolver
    {
        private readonly Func<IContainer> _containerfactory;

        public StructureMapDependancyResolver(Func<IContainer> containerfactory)
        {
            _containerfactory = containerfactory;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            var container = _containerfactory();

            return serviceType.IsAbstract || serviceType.IsInterface
                ? IoC.Container.TryGetInstance(serviceType)
                : IoC.Container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return IoC.Container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}