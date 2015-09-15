using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Contains common objects made available when creating resource model. It is 
    /// shared by the entire resource hierarchy as resources and routes are created.
    /// </summary>
    public class ConfigurationContext
    {
        public ConfigurationContext(CustomValueCollection sharedExtensionData)
        {
            SharedExtensionData = sharedExtensionData;
            Cache = new CustomValueCollection();
        }

        /// <summary>
        /// Shared convention data specified on the root resource
        /// </summary>
        public CustomValueCollection SharedExtensionData { get; set; }

        /// <summary>
        /// A collection of data shared by all builders in the hierarchy as
        /// the resource model is built - suitable for optimizations such as 
        /// caching shared data.
        /// </summary>
        public CustomValueCollection Cache { get; private set; }
    }
}