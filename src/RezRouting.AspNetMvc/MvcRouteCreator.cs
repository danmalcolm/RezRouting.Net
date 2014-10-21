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
        public void CreateRoutes(ResourcesModel model, RouteCollection routes, string area)
        {
            foreach (var route in GetRoutes(model.Resources))
            {
                CreateRoute(route, routes, area);
            }
        }

        private IEnumerable<Route> GetRoutes(IEnumerable<Resource> resources)
        {
            return resources.SelectMany(x => x.Routes.Concat(GetRoutes(x.Children)));
        }

        private void CreateRoute(Route model, RouteCollection routes, string area)
        {
            string controller = RouteValueHelper.TrimControllerFromTypeName(model.ControllerType);
            var defaults = new { controller = controller, action = model.Action };
            var constraints = CreateConstraints(model);
            var route = routes.MapRoute(model.FullName, model.Url, defaults, constraints);

            route.DataTokens[RouteDataTokenKeys.Namespaces] = new[] {model.ControllerType.Namespace};
            route.DataTokens[RouteDataTokenKeys.UseNamespaceFallback] = false;
            route.DataTokens[RouteDataTokenKeys.RouteModel] = model;
            route.DataTokens["Name"] = model.FullName;
            if (area != null)
            {
                route.DataTokens[RouteDataTokenKeys.Area] = area;
            }
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