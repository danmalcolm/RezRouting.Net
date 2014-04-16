namespace RezRouting.Model
{
    /// <summary>
    /// Common properties applied to Routes for a resource
    /// </summary>
    internal class RouteUrlProperties
    {
        public RouteUrlProperties(string path, string idName, string idNameAsAncestor)
        {
            Path = path;
            IdName = idName;
            IdNameAsAncestor = idNameAsAncestor;
        }

        public string Path { get; private set; }

        public string IdName { get; private set; }

        public string IdNameAsAncestor { get; private set; }
    }
}