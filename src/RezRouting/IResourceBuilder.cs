namespace RezRouting
{
    public interface IResourceBuilder
    {
        Resource Build(RouteMappingContext context);
    }
}