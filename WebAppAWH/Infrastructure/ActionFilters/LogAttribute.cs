using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWH.Infrastructure.ActionFilters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public ApplicationDbContext context { get; set; }
        public ICurrentUser currentuser { get; set; }
        private IDictionary<string, object> parameters { get; set; }
        public string Description { get; set; }

        public LogAttribute(string _description)
        {
            Description = _description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            parameters = filterContext.ActionParameters;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var description = Description;

            foreach (var kvp in parameters)
            {
                description = description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
            }

            context.Logs.Add(new LogAction(currentuser.GetCurrentUser(),
                                            filterContext.ActionDescriptor.ActionName,
                                            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                            description));

            context.SaveChanges();
        }
    }
}