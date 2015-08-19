using RezRouting.Configuration.Options;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Configures and creates a collection item resource
    /// </summary>
    public class CollectionItemBuilder : ResourceBuilderBase<CollectionItemData>, ICollectionItemConfigurator
    {
        /// <summary>
        /// Creates a new CollectionItemBuilder
        /// </summary>
        /// <param name="name"></param>
        public CollectionItemBuilder(ResourceData parentData, string name)
            : base(parentData, name)
        {
        }

        /// <inheritdoc />
        public void IdName(string name)
        {
            Data.CustomIdName = name;
        }

        /// <inheritdoc />
        public void IdNameAsAncestor(string name)
        {
            Data.CustomIdNameAsAncestor = name;
        }
    }
}