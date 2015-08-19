using System;
using RezRouting.Utility;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Configures and creates a collection Resource
    /// </summary>
    public class CollectionBuilder : ResourceBuilderBase<CollectionData>, ICollectionConfigurator
    {
        private readonly CollectionItemBuilder itemBuilder;
        
        /// <summary>
        /// Creates a new CollectionBuilder
        /// </summary>
        /// <param name="name"></param>
        public CollectionBuilder(ResourceData parentData, string name, string itemName = null) : base(parentData, name)
        {
            if (name == null) throw new ArgumentNullException("name");
            // Use specified name, attempt to singularise, or append Item if it can't be singularised
            itemName = itemName ?? name.Singularize() ?? string.Format("{0}Item", name);
            itemBuilder = new CollectionItemBuilder(Data, itemName);
            AddChild(itemBuilder, x => {});
        }

        /// <inheritdoc />
        public void Items(Action<ICollectionItemConfigurator> configure)
        {
            if (configure == null) throw new ArgumentNullException("configure");

            configure(itemBuilder);
        }

        /// <inheritdoc />
        public void UrlPath(string path)
        {
            Data.UrlPath = path;
        }
    }
}