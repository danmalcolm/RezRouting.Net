using RezRouting.Configuration.Builders;

namespace RezRouting.Configuration
{
    /// <summary>
    /// RezRouting's main entry point for configuring an application or component's resource hierarchy and routes.
    /// Configures routes and other attributes of the root resource and it's descendants.
    /// </summary>
    public class ResourceGraphBuilder : SingularBuilder
    {
        /// <summary>
        /// Creates a new ResourceGraphBuilder, RezRouting's main entry point for configuring an application
        /// or component's resource hierarchy and routes.
        /// </summary>
        /// <param name="name">An option name to give to the root resource. The specified name will be included in the 
        /// full name of all resources and routes set up by this ResourceGraphBuilder.</param>
        /// <returns></returns>
        public ResourceGraphBuilder(string name = "") : base(name)
        {
            this.UrlPath("");
        }
    }
}