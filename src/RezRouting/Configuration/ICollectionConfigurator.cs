using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up attributes of a collection resource during resource configuration.
    /// </summary>
    public interface ICollectionConfigurator : IResourceConfigurator
    {
        /// <summary>
        /// Configures resource representing items within this collection resource
        /// </summary>
        /// <param name="configure"></param>
        void Items(Action<ICollectionItemConfigurator> configure);

        /// <summary>
        /// Sets a custom path for the URL of this collection resource
        /// </summary>
        /// <param name="path"></param>
        void UrlPath(string path);
    }
}