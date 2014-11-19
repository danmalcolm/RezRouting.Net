using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc.RouteConventions.Tasks
{
    public class TaskRouteConvention : IRouteConvention
    {
        public TaskRouteConvention(string name, ResourceLevel level, string action, string httpMethod)
        {
            Name = name;
            Level = level;
            Action = action;
            HttpMethod = httpMethod;
        }

        public string Name { get; set; }

        public ResourceLevel Level { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }
        
        public IEnumerable<Route> Create(Resource resource, IEnumerable<Type> controllerTypes, UrlPathFormatter pathFormatter)
        {
            if (resource.Level == Level)
            {
                foreach (var controllerType in controllerTypes)
                {
                    var supported = ActionMappingHelper.SupportsAction(controllerType, Action);
                    if (supported)
                    {
                        var path = GetPath(resource, controllerType, pathFormatter);
                        string controllerName = RouteValueHelper.TrimControllerFromTypeName(controllerType);
                        string name = string.Format("{0}.{1}", controllerName, Action);
                        var route = new Route(name, controllerType, Action, HttpMethod, path);
                        yield return route;
                    }
                }
            }
        }

        private string GetPath(Resource resource, Type controllerType, UrlPathFormatter pathFormatter)
        {
            string path = RouteValueHelper.TrimControllerFromTypeName(controllerType);
            var suffixes = GetPossibleResourceNameSuffixes(resource);
            path = suffixes.OrderBy(x => x.Length)
                .Where(suffix => path.EndsWith(suffix))
                .Select(suffix => path.Substring(0, path.Length - suffix.Length))
                .FirstOrDefault() ?? path;

            path = pathFormatter.FormatDirectoryName(path);

            return path;
        }

        private IEnumerable<string> GetPossibleResourceNameSuffixes(Resource resource)
        {
            yield return resource.Name;
            if (resource.Level == ResourceLevel.Collection)
            {
                // Allow some flexibility of controller names used for collection tasks, e.g. NewProduct / NewProducts
                // both apply to the collection resource
                string singularName = resource.Name.Singularize(Plurality.Plural);
                yield return singularName;
            }
        }
    }
}