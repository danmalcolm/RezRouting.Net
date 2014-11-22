using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures a collection resource
    /// </summary>
    public interface IConfigureCollection : IConfigureResource
    {
        /// <summary>
        /// Configures resources at item level within this collection
        /// </summary>
        /// <param name="configure"></param>
        void Items(Action<CollectionItemBuilder> configure);

        /// <summary>
        /// Sets a custom path within the URL that identifies this collection.
        /// If a custom path is not set, then the URL is based on the name of the
        /// resource.
        /// </summary>
        /// <param name="path"></param>
        void UrlPath(string path);
    }
}