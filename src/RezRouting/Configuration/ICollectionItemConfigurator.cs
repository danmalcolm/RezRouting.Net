namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up attributes of a collection item resource during resource configuration.
    /// </summary>
    public interface ICollectionItemConfigurator : IResourceConfigurator
    {
        /// <summary>
        /// Sets the name of the identifier parameter used in route URLs for
        /// this resource
        /// </summary>
        /// <param name="name"></param>
        void IdName(string name);

        /// <summary>
        /// Sets the name of the identifier parameter used in route URLs for
        /// descendents of this resource
        /// </summary>
        /// <param name="name"></param>
        void IdNameAsAncestor(string name);
    }
}