using System.Collections.Generic;
using System.Linq;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc
{
    internal static class RouteSorter
    {
        /// <summary>
        /// Returns routes within a resource hierarchy, ordered by sequence in 
        /// which the ASP.Net MVC routes should be created. Routes belonging to parent resources are
        /// created before those belonging to children. For each resource, routes belonging to
        /// child collection item resources are mapped after those belonging to other resource
        /// types. 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<Route> Sort(Resource root)
        {
            return GetRouteModelsInCreationOrder(new List<Resource> {root});
        }

        private static IEnumerable<Route> GetRouteModelsInCreationOrder(IList<Resource> resources)
        {
            var sortedResources = SortResourcesByRouteCreationOrder(resources);
            return sortedResources.SelectMany(x => x.Routes.Concat(GetRouteModelsInCreationOrder(x.Children)));
        }

        private static IEnumerable<Resource> SortResourcesByRouteCreationOrder(IList<Resource> resources)
        {
            return resources.Where(x => x.Type != ResourceType.CollectionItem)
                .Concat(resources.Where(x => x.Type == ResourceType.CollectionItem));
        }
    }
}