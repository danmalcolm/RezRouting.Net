using System.Collections.Generic;
using RezRouting2.Utility;

namespace RezRouting2
{
    public class Resource
    {
        public Resource(string name, string urlPath, ResourceLevel level, IEnumerable<Resource> children)
        {
            Name = name;
            UrlPath = urlPath;
            Level = level;
            Children = children.ToReadOnlyList();
            Children.Each(child => child.InitParent(this));
        }

        private void InitParent(Resource parent)
        {
            Parent = parent;
        }

        public string Name { get; private set; }

        public string UrlPath { get; private set; }

        public ResourceLevel Level { get; set; }

        public IList<Resource> Children { get; private set; }

        public Resource Parent { get; private set; }
    }
}