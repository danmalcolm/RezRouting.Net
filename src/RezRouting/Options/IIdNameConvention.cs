namespace RezRouting.Options
{
    /// <summary>
    /// Formats the name of the route value placeholder for the id of a collection item in
    /// a route URL.
    /// </summary>
    public interface IIdNameConvention
    {
        /// <summary>
        /// Gets the key for the id of the current resource for which route is being mapped
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetIdName(string resourceName);

        /// <summary>
        /// Gets the key for the id of an ancestor resource within a nested route
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetIdNameAsAncestor(string resourceName);
    }
}