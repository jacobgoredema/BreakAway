using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BreakAway.App.Startup))]
namespace BreakAway.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
