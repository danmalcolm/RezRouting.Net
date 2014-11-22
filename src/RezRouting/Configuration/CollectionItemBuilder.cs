using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures and creates cResources at collection item level
    /// </summary>
    public class CollectionItemBuilder : ResourceBuilder, IConfigureCollectionItem
    {
        private string customIdName;
        private string customIdNameAsAncestor;

        /// <summary>
        /// Creates a new CollectionItemBuilder
        /// </summary>
        /// <param name="name"></param>
        public CollectionItemBuilder(string name)
            : base(name, ResourceLevel.CollectionItem)
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