﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using RezRouting.AspNetMvc.UrlGeneration;
using RezRouting.AspNetMvc.Utility;
using RezRouting.Resources;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Adds ASP.Net MVC routes to RouteCollection based on routes belonging to a 
    /// hierarchy of resources
    /// </summary>
    public class MvcRouteCreator
    {
        /// <summary>
        /// Adds the routes within a hierarchy of resources to the specified RouteCollection
        /// </summary>
        /// <param name="root"></param>
        /// <param name="routes"></param>
        /// <param name="area">The name of the area - if null, no area will be configured</param>
        public void CreateRoutes(Resource root, RouteCollection routes, string area)
        {
            var routeModels = RouteSorter.Sort(root).ToList();

            new RouteValidator().ThrowIfInvalid(routeModels, routes);
            
            foreach (var route in routeModels)
            {
                CreateRoute(route, routes, area);
            }
            UrlHelperExtensions.Index.AddRoutes(routes);
        }
        
        private void CreateRoute(Route model, RouteCollection routes, string area)
        {
            var handler = model.Handler as MvcAction;
            if (handler != null)
            {
                var controllerType = handler.ControllerType;
                string controller = RouteValueHelper.TrimControllerFromTypeName(controllerType);
                var defaults = new RouteValueDictionary(model.AdditionalRouteValues);
                defaults[RouteValueKeys.Controller] = controller;
                defaults[RouteValueKeys.Action] = handler.ActionName;

                var constraints = CreateConstraints(model);

                var route = new ResourceRoute(model.FullName, model.Url, new MvcRouteHandler())
                {
                    Defaults = defaults,
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