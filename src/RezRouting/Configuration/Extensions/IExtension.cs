using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Options;

namespace RezRouting.Configuration.Extensions
{
    /// <summary>
    /// An extension mechanism for adding custom functionality during Resource and 
    /// Route configuration.
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// Applies custom configuration logic to the root resource and its descendants
        /// during route configuration. 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="context"></param>
        /// <param name="options"></param>
        void Extend(ResourceData root, ConfigurationContext context, ConfigurationOptions options);
    }
}