namespace RezRouting.Configuration.Options
{
    /// <summary>
    /// Formats the name of the id parameter placeholder for URL template of a collection item level
    /// Resource, based on the name of the Resource
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