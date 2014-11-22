namespace RezRouting.Configuration
{
    /// <summary>
    /// An individual element within a resource URL. Different types of segments
    /// are combined to form a resource URL.
    /// </summary>
    public interface IUrlSegment
    {
        /// <summary>
        /// The path with the URL template
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Path within the URL template when generating a URL for a child resource
        /// (there is a variation between the id name)
        /// </summary>
        string PathAsAncestor { get; }
    }
}