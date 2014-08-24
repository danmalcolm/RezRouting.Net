using RezRouting2.Utility;

namespace RezRouting2
{
    public class CollectionItemBuilder : ResourceBuilder
    {
        public CollectionItemBuilder(string name)
            : base(name, ResourceLevel.CollectionItem)
        {
            UrlSegment = new IdUrlSegment("id", name.ToCamelCase() + "Id");
        }

        public void IdName(string name)
        {
            UrlSegment = new IdUrlSegment(name, name.ToCamelCase() + "Id");
        }
    }
}