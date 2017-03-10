using System.Web.Mvc;

namespace WebAppAWH.Areas.Invitee
{
    public class InviteeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Invitee";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Invitee_default",
                "Invitee/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebAppAWH.Areas.Invitee.Controllers" }
            );
        }
    }
}