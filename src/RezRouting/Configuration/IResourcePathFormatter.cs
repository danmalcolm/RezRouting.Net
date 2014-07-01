namespace RezRouting.Configuration
{
    /// <summary>
    /// Formats the path-segment within a resource's URL
    /// </summary>
    public interface IResourcePathFormatter
    {
        string GetResourcePath(string name);
    }
}