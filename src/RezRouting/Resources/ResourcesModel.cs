using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Resources
{
    /// <summary>
    /// Contains models representing the Resources and Routes configured for an application
    /// (or part of an application). Used internally but also available for use by any 
    /// application-specific functionality.
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
        /// A collection containing the top-level Resources within the hierarchy
        /// </summary>
        public IList<Resource> Resources { get; private set; }  
    }
}