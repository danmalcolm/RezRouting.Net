using System.Linq;
using RezRouting.Configuration;
using RezRouting.Model;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Configures routes for the actions available on a collection of resources
    /// </summary>
    public class CollectionBuilder : ResourceBuilder
    {
        private CollectionItemBuilder itemBuilder;

        protected override ResourceType ResourceType
        {
            get { return ResourceType.Collection;  }
        }

        /// <summary>
        /// Specifies the name of the id value parameter used to specify an item in this 
        /// collection within the route URL, e.g. orders/{id}. This defaults to "id", e.g. orders/{id} but
        /// can be overriden.
        /// </summary>
        /// <param name="name"></param>
        public void IdName(string name)
        {
            CustomIdName(name);
        }

        /// <summary>
        /// Specifies the name of the id value parameter used to specify an item in this 
        /// collection within the route URL of a child resource, e.g. product/{productId}/reviews/{id}.
        /// This controls the name of the route value made available to the controller action. This 
        /// defaults to the singular name of the resource + "Id" but can be overriden.
        /// </summary>
        /// <param name="name"></param>
        public void IdNameAsAncestor(string name)
        {
            CustomIdNameAsAncestor(name);
        }

        protected override Resource BuildResource(RouteConfiguration configuration, ResourceBuildContext context)
        {
            var resourceName = GetResourceName(configuration);
            string routeResourceName = resourceName.Plural;

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
    /// <summary>
    /// Configures routes for the actions available on a collection of resources
    /// </summary>
    public class CollectionItemBuilder : ResourceBuilder
    {
        protected override ResourceType ResourceType
        {
            get { return ResourceType.Collection; }
        }

        /// <summary>
        /// Specifies the name of the id value parameter used to specify an item in this 
        /// collection within the route URL, e.g. orders/{id}. This defaults to "id", e.g. orders/{id} but
        /// can be overriden.
        /// </summary>
        /// <param name="name"></param>
        public void IdName(string name)
        {
            CustomIdName(name);
        }

        /// <summary>
        /// Specifies the name of the id value parameter used to specify an item in this 
        /// collection within the route URL of a child resource, e.g. product/{productId}/reviews/{id}.
        /// This controls the name of the route value made available to the controller action. This 
        /// defaults to the singular name of the resource + "Id" but can be overriden.
        /// </summary>
        /// <param name="name"></param>
        public void IdNameAsAncestor(string name)
        {
            CustomIdNameAsAncestor(name);
        }

        protected override Resource BuildResource(RouteConfiguration configuration, ResourceBuildContext context)
        {
            var resourceName = GetResourceName(configuration);
            string routeResourceName = resourceName.Plural;

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