namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up attributes of a singular resource during resource configuration.
    /// </summary>
    public interface ISingularConfigurator : IResourceConfigurator
    {
        /// <summary>
        /// Sets a custom path within the URL that identifies this resource.
        /// The path is based on the name of the resource by default and can be
        /// overridden using this method.
        /// </summary>
        /// <param name="path"></param>
        void UrlPath(string path);
    }
}