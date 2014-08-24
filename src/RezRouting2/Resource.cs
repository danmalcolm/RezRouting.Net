using System.Collections.Generic;
using RezRouting2.Utility;

namespace RezRouting2
{
    public class Resource
    {
        private readonly IUrlSegment urlSegment;

        public Resource(string name, IUrlSegment urlSegment, ResourceLevel level, IEnumerable<Resource> children)
        {
            Name = name;
            this.urlSegment = urlSegment;
            Level = level;
            Children = children.ToReadOnlyList();
            Children.Each(child => child.InitParent(this));
        }

        private void InitParent(Resource parent)
        {
            Parent = parent;
        }

        public string Name { get; private set; }

        public string UrlPath
        {
            get
            {
                // TODO - optimise (create once on construction?)
                string parentPath = Parent != null ? Parent.UrlPathAsAncestor : "";
                return string.Concat(parentPath, string.IsNullOrWhiteSpace(parentPath) ? "" : "/", urlSegment.Path);
            }
        }

        public string UrlPathAsAncestor
        {
            get
            {
                // TODO - optimise (create once on construction?)
                string parentPath = Parent != null ? Parent.UrlPathAsAncestor : "";
                return string.Concat(parentPath, string.IsNullOrWhiteSpace(parentPath) ? "" : "/", urlSegment.PathAsAncestor);
            }
        }

        public ResourceLevel Level { get; private set; }

        public IList<Resource> Children { get; private set; }

        public Resource Parent { get; private set; }
    }
}