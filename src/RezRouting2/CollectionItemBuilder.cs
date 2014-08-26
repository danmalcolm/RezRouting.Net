using RezRouting2.Utility;

namespace RezRouting2
{
    public class CollectionItemBuilder : ResourceBuilder
    {
        private string idName = "";
        private string idNameAsAncestor = "";

        public CollectionItemBuilder(string name)
            : base(name, ResourceLevel.CollectionItem)
        {
            idName = "id";
            idNameAsAncestor = name.ToCamelCase() + "Id";
            UrlSegment = new IdUrlSegment(idName, idNameAsAncestor);
        }

        public void IdName(string name)
        {
            idName = name;
            UrlSegment = new IdUrlSegment(idName, idNameAsAncestor);
        }

        public void IdNameAsAncestor(string name)
        {
            idNameAsAncestor = name;
            UrlSegment = new IdUrlSegment(idName, idNameAsAncestor);
        }
    }
}