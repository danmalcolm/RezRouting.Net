namespace RezRouting.Model
{
    /// <summary>
    /// Properties to apply when generating a resource's route URL
    /// </summary>
    public class RouteUrlProperties
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