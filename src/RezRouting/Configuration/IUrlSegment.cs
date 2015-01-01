namespace RezRouting.Configuration
{
    /// <summary>
    /// An individual element within a resource's URL template. Different types of 
    /// IUrlSegment are combined to form a complete URL template.
    /// </summary>
    public interface IUrlSegment
    {
        /// <summary>
        /// Path within the URL template when generating a URL belonging to a resource
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Path within the URL template when generating a URL for a descendant resource.
        /// </summary>
        string PathAsAncestor { get; }
    }
}