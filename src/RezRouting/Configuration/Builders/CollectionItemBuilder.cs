using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Configures and creates a collection item resource
    /// </summary>
    public class CollectionItemBuilder : ResourceBuilderBase, ICollectionItemConfigurator
    {
        private string customIdName;
        private string customIdNameAsAncestor;

        /// <summary>
        /// Creates a new CollectionItemBuilder
        /// </summary>
        /// <param name="name"></param>
        public CollectionItemBuilder(string name)
            : base(name, ResourceType.CollectionItem)
        {
        }

        /// <inheritdoc />
        public void IdName(string name)
        {
            customIdName = name;
        }

        /// <inheritdoc />
        public void IdNameAsAncestor(string name)
        {
            customIdNameAsAncestor = name;
        }

        /// <inheritdoc />
        protected override IUrlSegment GetUrlSegment(ConfigurationOptions options)
        {
            string idName = customIdName ?? options.IdNameFormatter.GetIdName(Name);
            string idNameAsAncestor = customIdNameAsAncestor ?? options.IdNameFormatter.GetIdNameAsAncestor(Name);
            
            return new IdUrlSegment(idName, idNameAsAncestor);
        }
    }
}