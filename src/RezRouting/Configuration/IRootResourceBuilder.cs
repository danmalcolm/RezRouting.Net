using RezRouting.Configuration.Builders;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures routes and other attributes of the root resource and it's descendants.
    /// </summary>
    public interface IRootResourceBuilder : ISingularConfigurator, IResourceBuilder
    {
         
    }
}