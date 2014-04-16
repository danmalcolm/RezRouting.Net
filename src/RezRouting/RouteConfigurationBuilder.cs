using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration;

namespace RezRouting
{
    /// <summary>
    /// Configures settings applied to routes set up by a RouteMapper
    /// </summary>
    public class RouteConfigurationBuilder
    {
        private readonly List<RouteType> routeTypes;
        private IResourceNameConvention resourceNameConvention;
        private IResourcePathFormatter resourcePathFormatter;
        private string routeNamePrefix;
        
        public RouteConfigurationBuilder()
        {
            routeTypes = new List<RouteType>
            {
                new RouteType("Index", new[] { ResourceType.Collection }, CollectionLevel.Collection, "Index", "", StandardHttpMethod.Get, 0),
                new RouteType("Show", new[] { ResourceType.Singular, ResourceType.Collection }, CollectionLevel.Item, "Show", "", StandardHttpMethod.Get, 3),
                new RouteType("New", new[] { ResourceType.Singular, ResourceType.Collection }, CollectionLevel.Collection, "New", "new", StandardHttpMethod.Get, 1),
                new RouteType("Create", new[] { ResourceType.Singular, ResourceType.Collection }, CollectionLevel.Collection, "Create", "", StandardHttpMethod.Post, 4),
                new RouteType("Edit", new[] { ResourceType.Singular, ResourceType.Collection }, CollectionLevel.Item, "Edit", "edit", StandardHttpMethod.Get, 2),
                new RouteType("Update", new[] { ResourceType.Singular, ResourceType.Collection }, CollectionLevel.Item, "Update", "", StandardHttpMethod.Put, 5),
                new RouteType("Delete", new[] { ResourceType.Singular, ResourceType.Collection }, CollectionLevel.Item, "Destroy", "", StandardHttpMethod.Delete, 6)
            };
            resourceNameConvention = new DefaultResourceNameConvention();
            resourcePathFormatter = new DefaultResourcePathFormatter(new ResourcePathSettings());
            routeNamePrefix = "";
        }
        
        public void AddCustomRoute(RouteType routeType)
        {
            var conflictingRouteTypes = routeTypes.Where(rt => rt.ConflictsWith(routeType));
            if (conflictingRouteTypes.Any())
            {
                throw new ArgumentException(string.Format(@"The route is not valid as it conflicts with the following: 
{0}
", string.Join(Environment.NewLine, conflictingRouteTypes.Select(x => x.UserSummary))));
            }
            routeTypes.Add(routeType);
        }

        /// <summary>
        /// Uses the supplied IResourceNameConvention implementation to get the resource name
        /// </summary>
        /// <param name="convention"></param>
        public void CustomiseResourceNames(IResourceNameConvention convention)
        {
            if (convention == null) throw new ArgumentNullException("convention");
            resourceNameConvention = convention;
        }

        /// <summary>
        /// Uses the supplied function to get the resource name.
        /// </summary>
        public void CustomiseResourceNames(Func<IEnumerable<Type>, ResourceType, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            resourceNameConvention = new CustomResourceNameConvention(create);
        }

        /// <summary>
        /// Use the supplied settings to format the resource path.
        /// </summary>
        /// <param name="settings"></param>
        public void FormatResourcePaths(ResourcePathSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            resourcePathFormatter = new DefaultResourcePathFormatter(settings);
        }

        /// <summary>
        /// Uses the supplied IResourcePathFormatter implementation to format the resource path
        /// </summary>
        /// <param name="formatter"></param>
        public void FormatResourcePaths(IResourcePathFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException("formatter");
            resourcePathFormatter = formatter;
        }

        /// <summary>
        /// Use the supplied function to format the resource path. The function is invoked
        /// with the name of the resource
        /// </summary>
        /// <param name="create"></param>
        public void FormatResourcePaths(Func<string, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            resourcePathFormatter = new CustomResourcePathFormatter(create);
        }

        public void PrefixRouteNames(string prefix)
        {
            routeNamePrefix = prefix;
        }

        internal RouteConfiguration Build()
        {
            return new RouteConfiguration(routeTypes, resourceNameConvention, resourcePathFormatter, routeNamePrefix);
        }
    }
}