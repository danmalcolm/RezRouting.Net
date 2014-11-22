using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using RezRouting.Configuration;
using RezRouting.Utility;
using Route = RezRouting.Resources.Route;

namespace RezRouting.AspNetMvc
{
    internal class RouteValidator
    {
        public void ThrowIfInvalid(List<Route> routeModels, RouteCollection routes)
        {
            EnsureUniqueRouteNames(routeModels);
            EnsureRouteNamesNotInUse(routeModels, routes);
        }

        private void EnsureUniqueRouteNames(List<Route> routeModels)
        {
            var routesWithNonUniqueNames = routeModels
                .GroupBy(x => x.FullName)
                .Where(x => x.Count() > 1)
                .ToList();

            if (routesWithNonUniqueNames.Any())
            {
                var message = new StringBuilder();
                message.AppendLine("Unable to add routes to RouteCollection because the following route names are not unique:");
                routesWithNonUniqueNames.Each(g =>
                {
                    string name = g.Key;
                    string resourceSummary = TextUtility.FormatList(g.Select(r => r.Resource.FullName), ", ", " and ");
                    message.AppendFormat("{0} - (defined on resources {1})", name, resourceSummary);
                    message.AppendLine();
                });
                throw new RouteConfigurationException(message.ToString());
            }
        }

        private void EnsureRouteNamesNotInUse(List<Route> routeModels, RouteCollection routes)
        {
            var routesWithNameInUse = routeModels
                .Where(x => routes[x.FullName] != null)
                .ToList();
            if (routesWithNameInUse.Any())
            {
                var message = new StringBuilder();
                message.AppendLine("Unable to create routes because the following routes have names that already exist in the RouteCollection:");
                routesWithNameInUse.Each(route =>
                {
                    message.AppendFormat("{0} - (defined on resource {1})", route.FullName, route.Resource.FullName);
                    message.AppendLine();
                });
                throw new RouteConfigurationException(message.ToString());
            }

        }
    }
}