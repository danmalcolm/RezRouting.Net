using System.Linq;
using RezRouting.Configuration;
using RezRouting.Model;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Configures routes for the actions available on a singular resource
    /// </summary>
    public class SingularBuilder : ResourceBuilder
    {
        protected override ResourceType ResourceType
        {
            get { return ResourceType.Singular; }
        }

        protected override Resource BuildResource(RouteConfiguration configuration, ResourceBuildContext context)
        {
            var resourceName = GetResourceName(configuration);
            string routeResourceName = ResourceType == ResourceType.Collection
                ? resourceName.Plural
                : resourceName.Singular;

            var routeUrlProperties = GetRouteUrlProperties(configuration, resourceName);

            string ancestorPath = string.Join(".", context.AncestorNames);
            string fullResourceName = string.Format("{0}.{1}.{2}",
                configuration.RouteNamePrefix, ancestorPath, routeResourceName);

            var resourceNames = context.AncestorNames.Append(routeResourceName);
            var routes = GetRoutes(resourceName, resourceNames, configuration);

            var resource = new Resource(fullResourceName, context.Parent, routeUrlProperties, ResourceType, routes);
            var childContext = new ResourceBuildContext(resourceNames, resource, context.SharedConfiguration);
            var childResources = children.Select(x => x.Build(context.SharedConfiguration, childContext));
            resource.SetChildren(childResources);
            return resource;

        }
    }
}