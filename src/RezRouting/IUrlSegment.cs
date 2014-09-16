namespace RezRouting
{
    public interface IUrlSegment
    {
        string Path { get; }

        string PathAsAncestor { get; }
    }
}