using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Adds routes to RouteCollection based on resources and routes in a ResourcesModel
    /// </summary>
    public class MvcRouteCreator
    {
        public void CreateRoutes(ResourcesModel model, RouteCollection routes)
        {
            foreach (var route in GetRoutes(model.Resources))
            {
                CreateRoute(route, routes);
            }
        }

        private IEnumerable<Route> GetRoutes(IEnumerable<Resource> resources)
        {
            return resources.SelectMany(x => x.Routes.Concat(GetRoutes(x.Children)));
        }

        private void CreateRoute(Route model, RouteCollection routes)
        {
            string controller = RouteValueHelper.TrimControllerFromTypeName(model.ControllerType);
            var defaults = new { controller = controller, action = model.Action };
            var constraints = CreateConstraints(model);

            var route = routes.MapRoute(model.FullName, model.Url, defaults, constraints);
            route.Constraints = constraints;
            route.DataTokens["Namespaces"] = new[] {model.ControllerType.Namespace};
            route.DataTokens["UseNamespaceFallback"] = false;
            route.DataTokens["RouteModel"] = model;
            route.DataTokens["Name"] = model.FullName;
        }

        private RouteValueDictionary CreateConstraints(Route model)
        {
            string httpMethod = model.HttpMethod;
            var constraints = new RouteValueDictionary();
            constraints["httpMethod"] = new HttpMethodOrOverrideConstraint(httpMethod);
            return constraints;
        }
    }
}