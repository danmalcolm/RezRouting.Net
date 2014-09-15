using System.Web.Mvc;
using System.Web.Routing;

namespace RezRouting.Demos.Tasks
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngineConfig.Init(System.Web.Mvc.ViewEngines.Engines);
        }
    }
}
