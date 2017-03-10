using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppAWH.Startup))]
namespace WebAppAWH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
