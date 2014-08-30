namespace RezRouting2
{
    public interface IResourceBuilder
    {
        Resource Build(RouteMappingContext context);
    }
}