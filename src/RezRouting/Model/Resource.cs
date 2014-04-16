using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    internal class Resource
    {
        private readonly string fullName;
        private readonly Resource parent;
        private readonly RouteUrlProperties urlProperties;
        private readonly ResourceType resourceType;
        private readonly IList<ResourceRoute> routes;
        private IList<Resource> children;

        public Resource(string fullName, Resource parent, RouteUrlProperties urlProperties, ResourceType resourceType, IEnumerable<ResourceRoute> routes)
        {
            this.fullName = fullName;
            this.parent = parent;
            this.urlProperties = urlProperties;
            this.resourceType = resourceType;
            this.routes = routes.ToReadOnlyList();
        }

        internal void SetChildren(IEnumerable<Resource> resources)
        {
            this.children = resources.ToReadOnlyList();
        }

        public void MapRoutes(RouteCollection routeCollection)
        {
            foreach (var route in routes)
            {
                string url = GetUrl(route.RouteType);
                route.MapRoute(fullName, url, routeCollection);
            }
            foreach (var child in children)
            {
                child.MapRoutes(routeCollection);                
            }
        }

        private string GetUrl(RouteType routeType)
        {
            var url = new StringBuilder();
            if (parent != null)
            {
                url.Append(parent.GetUrlAsAncestor());
            }
            if (url.Length > 0) url.Append("/");
            url.Append(urlProperties.Path);

            bool includeId = resourceType == ResourceType.Collection
                             && routeType.CollectionLevel == CollectionLevel.Item;
            if (includeId)
            {
                if (url.Length > 0) url.Append("/");
                url.AppendFormat("{{{0}}}", urlProperties.IdName);
            }
            return url.ToString();
        }

        private string GetUrlAsAncestor()
        {
            var url = new StringBuilder();
            if (parent != null)
            {
                url.Append(parent.GetUrlAsAncestor());
            }
            if (url.Length > 0 && urlProperties.Path.Length > 0) url.Append("/");
            url.Append(urlProperties.Path);

            bool includeId = resourceType == ResourceType.Collection;
            if (includeId)
            {
                if (url.Length > 0) url.Append("/");
                url.AppendFormat("{{{0}}}", urlProperties.IdNameAsAncestor);
            }
            return url.ToString();
        }

        public void DebugSummary(StringBuilder summary, int level)
        {
            string indent = "".PadLeft(level, ' ');
            summary.AppendLine("-------------------------------------------------------------------------------");
            summary.Append(indent);
            summary.AppendFormat(@"""{0}"" ({1}) (~/{2})", fullName, resourceType, urlProperties.Path);
            summary.AppendLine();
            summary.AppendLine();
            foreach (var route in routes)
            {
                summary.Append(indent);
                string resourceUrlPath = GetUrl(route.RouteType);
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