namespace RezRouting.Options
{
    /// <summary>
    /// Formats the name of the route value placeholder for the id of a collection item in
    /// a route URL.
    /// </summary>
    public interface IIdNameConvention
    {
        /// <summary>
        /// Gets the key for the id of the resource within the URL of a route that belongs
        /// directly to the resource
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetIdName(string resourceName);

        /// <summary>
        /// Gets the key for the id of the resource within the URL of a route that belongs
        /// to a descendant of the resource
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string GetIdNameAsAncestor(string resourceName);
    }
}