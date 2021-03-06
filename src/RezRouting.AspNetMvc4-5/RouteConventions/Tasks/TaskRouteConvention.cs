﻿using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.AspNetMvc.Utility;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc.RouteConventions.Tasks
{
    /// <summary>
    /// Route convention used to create a task-oriented route
    /// </summary>
    public class TaskRouteConvention : MvcRouteConvention
    {
        public TaskRouteConvention(string name, ResourceType type, string action, string httpMethod)
        {
            Name = name;
            Type = type;
            Action = action;
            HttpMethod = httpMethod;
        }

        /// <summary>
        /// Name of the route
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of Resource to which this convention applies
        /// </summary>
        public ResourceType Type { get; set; }

        /// <summary>
        /// The name of the action that the route is mapped to
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The HTTP method used in requests for this route
        /// </summary>
        public string HttpMethod { get; set; }

        protected override IEnumerable<RouteData> CreateRoutes(ResourceData resource, ConfigurationContext context, ConfigurationOptions options,
            IEnumerable<Type> controllerTypes)
        {
            var urlPathSettings = options.UrlPathSettings;
            if (resource.Type == Type)
            {
                foreach (var controllerType in controllerTypes)
                {
                    var supported = ActionMappingHelper.SupportsAction(controllerType, Action, context.Cache);
                    if (supported)
                    {
                        var path = GetPath(resource, controllerType, urlPathSettings);
                        string controllerName = RouteValueHelper.TrimControllerFromTypeName(controllerType);
                        string name = string.Format("{0}.{1}", controllerName, Action);
                        var handler = new MvcAction(controllerType, Action);
                        var route = new RouteData(name, HttpMethod, path, handler);
                        yield return route;
                    }
                }
            }
        }

        private string GetPath(ResourceData resource, Type controllerType, UrlPathSettings urlPathSettings)
        {
            string path = RouteValueHelper.TrimControllerFromTypeName(controllerType);
            var suffixes = GetPossibleResourceNameSuffixes(resource);
            path = suffixes.OrderBy(x => x.Length)
                .Where(suffix => path.EndsWith(suffix))
                .Select(suffix => path.Substring(0, path.Length - suffix.Length))
                .FirstOrDefault() ?? path;

            path = urlPathSettings.FormatDirectoryName(path);

            return path;
        }

        private IEnumerable<string> GetPossibleResourceNameSuffixes(ResourceData resource)
        {
            yield return resource.Name;
            if (resource.Type == ResourceType.Collection)
            {
                // Allow some flexibility of controller names used for collection tasks, e.g. NewProduct / NewProducts
                // both apply to the collection resource
                string singularName = resource.Name.Singularize(Plurality.Plural);
                yield return singularName;
            }
        }
    }
}