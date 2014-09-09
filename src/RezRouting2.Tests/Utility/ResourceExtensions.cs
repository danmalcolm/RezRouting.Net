using System.Collections.Generic;
using System.Linq;

namespace RezRouting2.Tests.Utility
{
    public static class ResourceExtensions
    {

        /// <summary>
        /// Flattens resource hierarchy, returning all resources in the specified sequence and their descendants
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static IEnumerable<Resource> Expand(this IEnumerable<Resource> resources)
        {
            return resources.SelectMany(resource => new [] { resource }.Concat(resource.Children.Expand()));
        }

        /// <summary>
        /// Flattens resource hierarchy, returning the specified resource and its descendants
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static IEnumerable<Resource> Expand(this Resource resource)
        {
            return new [] { resource }.Expand();
        }
    }
}