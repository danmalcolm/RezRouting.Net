using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Options;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc.RouteTypes.Tasks
{
    public class TaskRouteType : IRouteType
    {
        public TaskRouteType(string name, ResourceLevel level, string action, string httpMethod)
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
        
        public Route BuildRoute(Resource resource, Type handlerType, UrlPathFormatter pathFormatter)
        {
            if (resource.Level == Level)
            {
                var supported = ActionMappingHelper.SupportsAction(handlerType, Action);
                if (supported)
                {
                    var builder = new RouteBuilder(handlerType);
                    var path = GetPath(resource, handlerType, pathFormatter);
                    string controllerName = RouteValueHelper.TrimControllerFromTypeName(handlerType);
                    string name = string.Format("{0}.{1}", controllerName, Action);
                    builder.Configure(name, Action, HttpMethod, path);
                    return builder.Build();
                }
            }
            return null;
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