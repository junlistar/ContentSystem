using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContentSystem.Startup))]
namespace ContentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
