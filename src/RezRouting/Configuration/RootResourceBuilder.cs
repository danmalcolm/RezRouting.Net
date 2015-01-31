using RezRouting.Configuration.Builders;

namespace RezRouting.Configuration
{
    /// <summary>
    /// RezRouting's main entry point for configuring an application or component's resource hierarchy and routes.
    /// Configures routes and other attributes of the root resource and it's descendants.
    /// </summary>
    public class RootResourceBuilder : SingularBuilder, IRootResourceBuilder
    {
        /// <summary>
        /// Creates a RootResourceBuilder.Create, RezRouting's main entry point for 
        /// configuring an application or component's resource hierarchy and routes.
        /// </summary>
        /// <param name="name">An optional name to give to the root resource. The specified 
        /// name will be included in the full name of all resources and routes set up by 
        /// this RootResourceBuilder.</param>
        /// <returns></returns>
        public static IRootResourceBuilder Create(string name = "")
        {
            return new RootResourceBuilder(name);
        }

        private RootResourceBuilder(string name = "") : base(name)
        {
            this.UrlPath("");
        }
    }
}