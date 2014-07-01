namespace RezRouting.Configuration
{
    /// <summary>
    /// Formats the name of the route value placeholder in URLs. This determines
    /// the key used in the RouteValueDictionary
    /// </summary>
    public interface IIdNameConvention
    {
        /// <summary>
        /// Gets the key for the id of the current resource for which route is being mapped
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetIdName(ResourceName resourceName);

        /// <summary>
        /// Gets the key for the id of an ancestor resource within a nested route
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetIdNameAsAncestor(ResourceName resourceName);
    }
}