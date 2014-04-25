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
        private readonly Setting<IResourceNameConvention> resourceNameConvention;
        private readonly Setting<IResourcePathFormatter> resourcePathFormatter; 
        private readonly Setting<string> routeNamePrefix; 
        
        public RouteConfigurationBuilder(IEnumerable<RouteType> routeTypes = null)
        {
            this.routeTypes = routeTypes != null 
                ? routeTypes.ToList() 
                : new List<RouteType>();
            resourceNameConvention = new Setting<IResourceNameConvention>();
            resourcePathFormatter = new Setting<IResourcePathFormatter>();
            routeNamePrefix = new Setting<string>();
        }

        /// <summary>
        /// Removes all RouteTypes
        /// </summary>
        public void ClearRoutes()
        {
            routeTypes.Clear();
        }

        /// <summary>
        /// Adds a new RouteType
        /// </summary>
        /// <param name="routeType"></param>
        public void AddRoute(RouteType routeType)
        {
            var conflictingRouteTypes = routeTypes
                .Where(rt => rt.ConflictsWith(routeType))
                .ToArray();
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
            resourceNameConvention.Set(convention);
        }

        /// <summary>
        /// Uses the supplied function to get the resource name.
        /// </summary>
        public void CustomiseResourceNames(Func<IEnumerable<Type>, ResourceType, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            var convention = new CustomResourceNameConvention(create);
            resourceNameConvention.Set(convention);
        }

        /// <summary>
        /// Use the supplied settings to format the resource path.
        /// </summary>
        /// <param name="settings"></param>
        public void FormatResourcePaths(ResourcePathSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            var formatter = new DefaultResourcePathFormatter(settings);
            resourcePathFormatter.Set(formatter);
        }

        /// <summary>
        /// Uses the supplied IResourcePathFormatter implementation to format the resource path
        /// </summary>
        /// <param name="formatter"></param>
        public void FormatResourcePaths(IResourcePathFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException("formatter");
            resourcePathFormatter.Set(formatter);
        }

        /// <summary>
        /// Use the supplied function to format the resource path. The function is invoked
        /// with the name of the resource
        /// </summary>
        /// <param name="create"></param>
        public void FormatResourcePaths(Func<string, string> create)
        {
            if (create == null) throw new ArgumentNullException("create");
            resourcePathFormatter.Set(new CustomResourcePathFormatter(create));
        }

        public void PrefixRouteNames(string prefix)
        {
            routeNamePrefix.Set(prefix);
        }

        internal RouteConfiguration Build()
        {
            return new RouteConfiguration(routeTypes, 
                resourceNameConvention.GetOrDefault(new DefaultResourceNameConvention()),
                resourcePathFormatter.GetOrDefault(new DefaultResourcePathFormatter(new ResourcePathSettings())),
                routeNamePrefix.GetOrDefault(""));
        }

        /// <summary>
        /// Creates a new RouteConfiguration instance based on an existing
        /// instance, overriding any of the configuration options specified.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal RouteConfiguration Extend(RouteConfiguration configuration)
        {
            var routeTypes2 = configuration.RouteTypes.Concat(routeTypes);
            var resourceNameConvention2 = resourceNameConvention.GetOrDefault(configuration.ResourceNameConvention);
            var resourcePathFormatter2 = resourcePathFormatter.GetOrDefault(configuration.ResourcePathFormatter);
            var routeNamePrefix2 = routeNamePrefix.GetOrDefault(configuration.RouteNamePrefix);
            
            return new RouteConfiguration(routeTypes2, resourceNameConvention2, resourcePathFormatter2, routeNamePrefix2);
        }

        internal class Setting<T>
        {
            private T value;

            internal bool IsSet { get; private set; }
           
            internal void Set(T setting)
            {
                IsSet = true;
                value = setting;
            }

            public T GetOrDefault(T @default)
            {
                return IsSet ? value : @default;
            }
        }

    }

    
}