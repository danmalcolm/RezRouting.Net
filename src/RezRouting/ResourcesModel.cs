using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Contains models representing the resources and routes configured by RouteMapper. This is 
    /// used internally but can also be used for any application-specific functionality that is
    /// based on the applications resources and routes.
    /// </summary>
    public class ResourcesModel
    {
        public ResourcesModel(IEnumerable<Resource> resources)
        {
            Resources = resources.ToReadOnlyList();
        }

        /// <summary>
        /// A collection of objects representing the resource and routes
        /// configured
        /// </summary>
        public IList<Resource> Resources { get; private set; }  
    }
}