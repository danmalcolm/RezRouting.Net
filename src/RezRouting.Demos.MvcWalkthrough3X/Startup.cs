using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RezRouting.Demos.MvcWalkthrough3.Startup))]
namespace RezRouting.Demos.MvcWalkthrough3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
