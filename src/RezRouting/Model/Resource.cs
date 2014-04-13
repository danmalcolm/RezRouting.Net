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
        private readonly string name;
        private string fullName;
        private readonly string path;
        private readonly ResourceType resourceType;
        private readonly string idName;
        private readonly string idNameAsAncestor;
        private readonly IList<Resource> ancestors;
        private readonly IList<ResourceRoute> routes;
        private IList<Resource> children;

        public Resource(IEnumerable<Resource> ancestors, string name, string path, ResourceType resourceType, string idName, string idNameAsAncestor, IEnumerable<ResourceRoute> routes)
        {
            this.ancestors = ancestors.ToReadOnlyList();
            this.name = name;
            this.fullName = string.Join(".", this.ancestors.Skip(1).Select(a => a.name).Concat(new[] {name}));
            this.path = path;
            this.resourceType = resourceType;
            this.idName = idName;
            this.idNameAsAncestor = idNameAsAncestor;
            this.routes = routes.ToReadOnlyList();
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
            if (url.Length > 0 && path.Length > 0) url.Append("/");
            url.Append(path);
            
            bool includeId = resourceType == ResourceType.Collection;
            if (includeId)
            {
                if (url.Length > 0) url.Append("/");
                url.AppendFormat("{{{0}}}", idNameAsAncestor);
            }
        }

        private void AppendUrlPathForRoute(StringBuilder url, RouteType routeType)
        {
            if (url.Length > 0) url.Append("/");
            url.Append(path);

            bool includeId = resourceType == ResourceType.Collection
                && routeType.CollectionLevel == CollectionLevel.Item;
            if (includeId)
            {
                if (url.Length > 0) url.Append("/");
                url.AppendFormat("{{{0}}}", idName);
            }
        }

        public void DebugSummary(StringBuilder summary, int level)
        {
            string indent = "".PadLeft(level, ' ');
            summary.AppendLine("-------------------------------------------------------------------------------");
            summary.Append(indent);
            summary.AppendFormat(@"""{0}"" ({1}) (~/{2})", name, resourceType, path);
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