using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using RezRouting.Utility;
using RezRouting.Configuration;

namespace RezRouting.Model
{
    /// <summary>
    /// Contains configuration and routes for a specific resource (singular or collection).
    /// An intermediate model created by the ResourceBuilders.
    /// </summary>
    public class Resource
    {
        private readonly ResourceRouteProperties routeProperties;
        private string fullName;
        private readonly ResourceType resourceType;
        private readonly IList<Resource> ancestors;
        private readonly IList<ResourceRoute> routes;
        private IList<Resource> children;

        public Resource(IEnumerable<Resource> ancestors, ResourceRouteProperties routeProperties, ResourceType resourceType, IEnumerable<ResourceRoute> routes)
        {
            this.ancestors = ancestors.ToReadOnlyList();
            this.routeProperties = routeProperties;
            this.fullName = FormatFullName();
            this.resourceType = resourceType;
            this.routes = routes.ToReadOnlyList();
        }

        private string FormatFullName()
        {
            var parts = new List<string>();
            if(!string.IsNullOrWhiteSpace(routeProperties.RouteNamePrefix))
                parts.Add(routeProperties.RouteNamePrefix);
            parts.AddRange(ancestors.Select(a => a.routeProperties.Name));
            parts.Add(routeProperties.Name);
            return string.Join(".", parts);
        }

        internal void SetChildren(IEnumerable<Resource> resources)
        {
            this.children = resources.ToReadOnlyList();
        }
        
        public void MapRoutes(RouteCollection routeCollection)
        {
            this.routes.Each(x => MapRoute(x, routeCollection));
            children.Each(x => x.MapRoutes(routeCollection));
        }

        private void MapRoute(ResourceRoute route, RouteCollection routeCollection)
        {
            string resourceUrlPath = GetResourceUrlPath(route.RouteType);
            route.MapRoute(fullName, resourceUrlPath, routeCollection);
        }
        
        private string GetResourceUrlPath(RouteType routeType)
        {
            var url = new StringBuilder();
            ancestors.Each(x => x.AppendUrlPathAsAncestor(url));
            AppendUrlPathForRoute(url, routeType);
            return url.ToString();
        }

        private void AppendUrlPathAsAncestor(StringBuilder url)
        {
            if (url.Length > 0 && routeProperties.Path.Length > 0) url.Append("/");
            url.Append(routeProperties.Path);
            
            bool includeId = resourceType == ResourceType.Collection;
            if (includeId)
            {
                if (url.Length > 0) url.Append("/");
                url.AppendFormat("{{{0}}}", routeProperties.IdNameAsAncestor);
            }
        }

        private void AppendUrlPathForRoute(StringBuilder url, RouteType routeType)
        {
            if (url.Length > 0) url.Append("/");
            url.Append(routeProperties.Path);

            bool includeId = resourceType == ResourceType.Collection
                && routeType.CollectionLevel == CollectionLevel.Item;
            if (includeId)
            {
                if (url.Length > 0) url.Append("/");
                url.AppendFormat("{{{0}}}", routeProperties.IdName);
            }
        }

        public void DebugSummary(StringBuilder summary, int level)
        {
            string indent = "".PadLeft(level, ' ');
            summary.AppendLine("-------------------------------------------------------------------------------");
            summary.Append(indent);
            summary.AppendFormat(@"""{0}"" ({1}) (~/{2})", routeProperties.Name, resourceType, routeProperties.Path);
            summary.AppendLine();
            summary.AppendLine();
            foreach (var route in routes)
            {
                summary.Append(indent);
                string resourceUrlPath = GetResourceUrlPath(route.RouteType);
                route.DebugSummary(fullName, resourceUrlPath, summary);
                summary.AppendLine();
                summary.AppendLine();
            }
            foreach (var child in children)
            {
                child.DebugSummary(summary, ++level);
            }
            summary.AppendLine("-------------------------------------------------------------------------------");
        }
    }
}