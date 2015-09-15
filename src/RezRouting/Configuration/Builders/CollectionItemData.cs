using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Contains editable properties of a collection item resource, a mutable representation used 
    /// during resource configuration by builders and extensions
    /// </summary>
    public class CollectionItemData : ResourceData
    {
        public override ResourceType Type
        {
            get { return ResourceType.CollectionItem; }
        }

        public string CustomIdName { get; set; }

        public string CustomIdNameAsAncestor { get; set; }

        protected override IUrlSegment GetUrlSegment(ConfigurationOptions options)
        {
            string idName = CustomIdName ?? options.IdNameFormatter.GetIdName(Name);
            string idNameAsAncestor = CustomIdNameAsAncestor ?? options.IdNameFormatter.GetIdNameAsAncestor(Name);
            return new IdUrlSegment(idName, idNameAsAncestor);
        }

        protected override ResourceData CreateCopy()
        {
            return new CollectionItemData
            {
                CustomIdName = CustomIdName,
                CustomIdNameAsAncestor = CustomIdNameAsAncestor
            };
        }
    }
}