using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Creates resource object - paired with IResourceConfigurator 
    /// </summary>
    public interface IResourceBuilder 
    {
        /// <summary>
        /// Creates a Resource object based on the configuration specified
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Resource Build(ResourceOptions options);
    }
}