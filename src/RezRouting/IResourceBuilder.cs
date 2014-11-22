namespace RezRouting
{
    /// <summary>
    /// Creates a Resource instance based on any configuration applied
    /// </summary>
    /// <remarks>
    /// Normally paired with IConfigureResource - separates configuration from creation
    /// </remarks>
    public interface IResourceBuilder
    {
        /// <summary>
        /// Creates the Resource
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Resource Build(RouteMappingContext context);
    }
}