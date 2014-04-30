using RezRouting.Model;

namespace RezRouting
{
    internal class ResourceBuildContext
    {
        public ResourceBuildContext(string[] ancestorNames, Resource parent)
        {
            AncestorNames = ancestorNames;
            Parent = parent;
        }

        public string[] AncestorNames { get; private set; }

        public Resource Parent { get; private set; }
    }
}