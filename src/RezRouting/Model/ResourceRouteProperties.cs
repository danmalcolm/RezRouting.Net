namespace RezRouting.Model
{
    /// <summary>
    /// Common properties applied to Routes for a resource
    /// </summary>
    public class ResourceRouteProperties
    {
        public ResourceRouteProperties(string routeNamePrefix, string name, string path, string idName, string idNameAsAncestor)
        {
            RouteNamePrefix = routeNamePrefix;
            Name = name;
            Path = path;
            IdName = idName;
            IdNameAsAncestor = idNameAsAncestor;
        }

        public string RouteNamePrefix { get; private set; }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public string IdName { get; private set; }

        public string IdNameAsAncestor { get; private set; }
    }
}