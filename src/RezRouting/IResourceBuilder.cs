namespace RezRouting
{
    /// <summary>
    /// Creates an instance of a resource based on settings configured
    /// for the resource
    /// </summary>
    public interface IResourceBuilder
    {
        Resource Build(RouteMappingContext context);
    }
}