using RezRouting.Options;

namespace RezRouting
{
    public class CollectionItemBuilder : ResourceBuilder, IConfigureCollectionItem
    {
        private string customIdName;
        private string customIdNameAsAncestor;

        public CollectionItemBuilder(string name)
            : base(name, ResourceLevel.CollectionItem)
        {
        }

        public void IdName(string name)
        {
            customIdName = name;
        }

        public void IdNameAsAncestor(string name)
        {
            customIdNameAsAncestor = name;
        }

        protected override IUrlSegment GetUrlSegment(RouteOptions options)
        {
            string idName = customIdName ?? options.IdNameConvention.GetIdName(Name);
            string idNameAsAncestor = customIdNameAsAncestor ?? options.IdNameConvention.GetIdNameAsAncestor(Name);
            
            return new IdUrlSegment(idName, idNameAsAncestor);
        }
    }
}