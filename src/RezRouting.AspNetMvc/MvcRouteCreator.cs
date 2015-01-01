using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.Resources;
using RezRouting.Utility;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Adds routes to RouteCollection based on routes within a ResourceGraphModel
    /// </summary>
    public class MvcRouteCreator
    {
        /// <summary>
        /// Creates the routes within a ResourceGraphModel within the specified RouteCollection
        /// </summary>
        /// <param name="model"></param>
        /// <param name="routes"></param>
        /// <param name="area">The name of the area - if null, no area will be configured</param>
        public void CreateRoutes(ResourceGraphModel model, RouteCollection routes, string area)
        {
            var routeModels = GetRoutes(model.Resources).ToList();

            new RouteValidator().ThrowIfInvalid(routeModels, routes);
            
            foreach (var route in routeModels)
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
            var handler = model.Handler as MvcAction;
            if (handler != null)
            {
                var controllerType = handler.ControllerType;
                string controller = RouteValueHelper.TrimControllerFromTypeName(controllerType);
                var constraints = CreateConstraints(model);

                var route = new ResourceRoute(model.Url, new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary
                    {
                        { "controller", controller }, 
                        { "action", handler.ActionName }
                    },
                    Constraints = constraints,
                    DataTokens = new RouteValueDictionary()
                };

                route.DataTokens[RouteDataTokenKeys.Namespaces] = new[] { controllerType.Namespace };
                route.DataTokens[RouteDataTokenKeys.UseNamespaceFallback] = false;
                route.DataTokens[RouteDataTokenKeys.RouteModel] = model;
                route.DataTokens["Name"] = model.FullName;
                if (area != null)
                {
                    route.DataTokens[RouteDataTokenKeys.Area] = area;
                }
                routes.Add(model.FullName, route);         
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