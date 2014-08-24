namespace RezRouting2
{
    public interface IUrlSegment
    {
        string Path { get; }

        string PathAsAncestor { get; }
    }
}