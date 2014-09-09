﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting2.Utility;

namespace RezRouting2.AspNetMvc
{
    public class MvcRouteMapper
    {
        public void CreateRoutes(IEnumerable<Resource> resources, RouteCollection routes)
        {
            foreach (var route in GetRoutes(resources))
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
            var defaults = new {controller = controller, action = model.Action};
            var constraints = GetConstraints(model);

            var route = new ResourceRoute(model.Name, model.Url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = constraints,
                DataTokens = new RouteValueDictionary {},
            };
            route.DataTokens["Namespaces"] = new[] {model.ControllerType.Namespace};
            route.DataTokens["UseNamespaceFallback"] = false;
            route.DataTokens["RouteModel"] = model;

            routes.Add(route);
        }

        private RouteValueDictionary GetConstraints(Route model)
        {
            string httpMethod = model.HttpMethod;
            var constraints = new RouteValueDictionary();
            constraints["httpMethod"] = new HttpMethodOrOverrideConstraint(httpMethod);
            return constraints;
        }
    }
}