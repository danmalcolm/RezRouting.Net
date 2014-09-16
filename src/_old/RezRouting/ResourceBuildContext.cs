using RezRouting.Configuration;
using RezRouting.Model;

namespace RezRouting
{
    public class ResourceBuildContext
    {
        public ResourceBuildContext(string[] ancestorNames, Resource parent, RouteConfiguration sharedConfiguration)
        {
            AncestorNames = ancestorNames;
            Parent = parent;
            SharedConfiguration = sharedConfiguration;
        }

        public string[] AncestorNames { get; private set; }

        public Resource Parent { get; private set; }

        public RouteConfiguration SharedConfiguration { get; private set; }
    }
}