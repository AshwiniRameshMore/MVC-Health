using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcHealth.Startup))]
namespace MvcHealth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
