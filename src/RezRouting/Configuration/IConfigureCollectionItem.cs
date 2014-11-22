namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures a collection item resource
    /// </summary>
    public interface IConfigureCollectionItem : IConfigureResource
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