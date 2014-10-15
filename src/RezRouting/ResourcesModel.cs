using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// A semantic model containing resources configured during mapping of resources and routes.
    /// This is used internally, but is also suitable for application-specific extensions
    /// based on resources and routes that have been set up.
    /// </summary>
    public class ResourcesModel
    {
        public ResourcesModel(IEnumerable<Resource> resources)
        {
            Resources = resources.ToReadOnlyList();
        }

        /// <summary>
        /// A collection of Resource representing the resources configured
        /// </summary>
        public IList<Resource> Resources { get; private set; }  
    }
}