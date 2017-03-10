using Data;
using Domain;
using StructureMap;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAppAWH.Infrastructure;
using WebAppAWH.Infrastructure.Tasks;

namespace WebAppAWH
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IContainer Container
        {
            get { return (IContainer)HttpContext.Current.Items["_Container"]; }
            set { HttpContext.Current.Items["_Container"] = value; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<ApplicationDbContext>(new AWHDbConfiguration());

            DependencyResolver.SetResolver(new StructureMapDependancyResolver(() => Container ?? IoC.Container));

            IoC.Container.Configure(cfg =>
                {
                    cfg.AddRegistry(new StandardRegistry());
                    cfg.AddRegistry(new MvcRegistry());
                    cfg.AddRegistry(new ServiceRegistry());
                    cfg.AddRegistry(new ActionFilterRegistry(
                    () => Container ?? IoC.Container));
                    cfg.AddRegistry(new RepositoryRegistry());
                    cfg.AddRegistry(new TaskRegistry());
                });

            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(force: true);
            }

            using (var container = IoC.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartUp>())
                {
                    task.Execute();
                }
            }
        }

        protected void Application_BeginRequest()
        {
            Container = IoC.Container.GetNestedContainer();
            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        protected void Application_EndRequest()
        {
            try
            {
                foreach (var task in Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }

        protected void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }
    }
}
