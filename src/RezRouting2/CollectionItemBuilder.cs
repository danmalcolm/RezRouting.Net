namespace RezRouting2
{
    public class CollectionItemBuilder : ResourceBuilder
    {
        public CollectionItemBuilder(string name)
            : base(name, ResourceLevel.CollectionItem)
        {
            UrlSegment = new IdUrlSegment("id", name + "Id");
        }

        public void IdName(string name)
        {
            UrlSegment = new IdUrlSegment(name, name);
        }
    }
}