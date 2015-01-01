using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures a collection resource
    /// </summary>
    public interface IConfigureCollection : IConfigureResource, IConfigureChildren
    {
        /// <summary>
        /// Sets the name used for the collection itemtype resource belonging to this 
        /// collection. The item name is based on a singular version of the collection 
        /// name by default - this method allows an alternative to be specified.
        /// </summary>
        /// <param name="name"></param>
        void ItemName(string name);

        /// <summary>
        /// Configures resource representing itemstype within this collection
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