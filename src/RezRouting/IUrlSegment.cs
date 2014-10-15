namespace RezRouting
{
    /// <summary>
    /// An individual element within a resource URL. Different types of segments
    /// are combined to form a resource URL.
    /// </summary>
    public interface IUrlSegment
    {
        string Path { get; }

        string PathAsAncestor { get; }
    }
}