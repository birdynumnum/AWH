using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWH.Infrastructure.Alerts
{
    public class AlertDecoratorResult : ActionResult
    {
        public ActionResult InnerResult { get; set; }
        public string AlertClass { get; set; }
        public string Message { get; set; }

        public AlertDecoratorResult(ActionResult innerresult, string alertclass, string message )
        {
            InnerResult = innerresult;
            AlertClass = alertclass;
            Message = message;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var alerts = context.Controller.TempData.GetAlerts();
            alerts.Add(new Alert(AlertClass, Message));
            InnerResult.ExecuteResult(context);
        }
    }
}