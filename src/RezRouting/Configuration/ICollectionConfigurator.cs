using System;
using RezRouting.Configuration.Builders;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up attributes of a collection resource during resource configuration.
    /// </summary>
    public interface ICollectionConfigurator : IResourceConfigurator
    {
        /// <summary>
        /// Configures resource representing itemstype within this collection
        /// </summary>
        /// <param name="configure"></param>
        void Items(Action<CollectionItemBuilder> configure);

        /// <summary>
        /// Sets a custom path within the URL that identifies this collection.
        /// The path is based on the name of the resource by default and can be
        /// overridden using this method.
        /// </summary>
        /// <param name="path"></param>
        void UrlPath(string path);
    }
}