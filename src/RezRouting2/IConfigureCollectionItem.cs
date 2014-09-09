namespace RezRouting2
{
    public interface IConfigureCollectionItem : IConfigureResource
    {
        void IdName(string name);
        void IdNameAsAncestor(string name);
    }
}