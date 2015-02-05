using System.Collections.Generic;
using System.Linq;
using RezRouting.Resources;

namespace RezRouting.Tests.Infrastructure
{
    public static class ResourceExtensions
    {
        /// <summary>
        /// Gets a "flattened" resource hierarchy, returning all resources in the 
        /// specified sequence and their descendants
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static IEnumerable<Resource> Expand(this IEnumerable<Resource> resources)
        {
            return resources.SelectMany(resource => new [] { resource }.Concat(Expand((IEnumerable<Resource>) resource.Children)));
        }

        /// <summary>
        /// Gets a "flattens" resource hierarchy, returning the specified resource 
        /// and its descendants
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static IEnumerable<Resource> Expand(this Resource resource)
        {
            return new [] { resource }.Expand();
        }
    }
}