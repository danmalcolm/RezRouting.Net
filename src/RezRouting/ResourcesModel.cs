using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Contains models representing the Resources and Routes configured for the application.
    /// Used internally but can also be used for any application-specific functionality.
    /// </summary>
    public class ResourcesModel
    {
        /// <summary>
        /// Creates a ResourceModel
        /// </summary>
        /// <param name="resources"></param>
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