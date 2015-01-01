using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures and creates a collection item resource
    /// </summary>
    public class CollectionItemBuilder : ResourceBuilderBase, IConfigureCollectionItem
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
        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string idName = customIdName ?? options.IdNameConvention.GetIdName(Name);
            string idNameAsAncestor = customIdNameAsAncestor ?? options.IdNameConvention.GetIdNameAsAncestor(Name);
            
            return new IdUrlSegment(idName, idNameAsAncestor);
        }
    }
}