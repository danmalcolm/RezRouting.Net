namespace RezRouting
{
    public interface IConfigureCollectionItem : IConfigureResource
    {
        void IdName(string name);
        void IdNameAsAncestor(string name);
    }
}