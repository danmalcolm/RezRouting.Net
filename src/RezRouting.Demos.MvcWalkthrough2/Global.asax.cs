using System.Web.Mvc;
using System.Web.Routing;

namespace RezRouting.Demos.MvcWalkthrough2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEnginesConfig.Configure(System.Web.Mvc.ViewEngines.Engines);
        }
    }
}
