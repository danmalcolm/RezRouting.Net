using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting
{
    /// <summary>
    /// Models the properties of a resource within an application's route configuration
    /// </summary>
    public class Resource
    {
        private readonly IUrlSegment urlSegment;

        /// <summary>
        /// Creates a Resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="urlSegment"></param>
        /// <param name="level"></param>
        /// <param name="customProperties"></param>
        /// <param name="children"></param>
        public Resource(string name, IUrlSegment urlSegment, ResourceLevel level, IDictionary<string,object> customProperties, IEnumerable<Resource> children)
        {
            Name = name;
            this.urlSegment = urlSegment;
            Level = level;
            CustomProperties = customProperties;
            Children = children.ToReadOnlyList();
            Children.Each(child => child.InitParent(this));
        }

        internal void InitRoutes(IEnumerable<Route> routes)
        {
            Routes = routes.ToReadOnlyList();
            Routes.Each(route => route.InitResource(this));
        }

        private void InitParent(Resource parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// The name of the resource
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The full unique name of the resource based on the names of the resource
        /// and its ancestors
        /// </summary>
        public string FullName
        {
            get {
                // TODO - optimise (create once on construction?)
                string parentFullName = Parent != null ? Parent.FullName : "";
                return string.Concat(parentFullName, string.IsNullOrWhiteSpace(parentFullName) ? "" : ".", Name);
            }
        }

        /// <summary>
        /// The URL used to identify this resource within the URL of a route belonging
        /// directly to this resource
        /// </summary>
        public string Url
        {
            get
            {
                // TODO - optimise (create once on construction?)
                string parentPath = Parent != null ? Parent.UrlAsAncestor : "";
                string path = UrlPathHelper.JoinPaths(parentPath, urlSegment.Path);
                return path;
            }
        }

        /// <summary>
        /// The URL used to identify this resource within the URL of a route belonging
        /// to a descendnt of this resource
        /// </summary>
        public string UrlAsAncestor
        {
            get
            {
                // TODO - optimise (create once on construction?)
                string parentPath = Parent != null ? Parent.UrlAsAncestor : "";
                string path = UrlPathHelper.JoinPaths(parentPath, urlSegment.PathAsAncestor);
                return path;
            }
        }

        /// <summary>
        /// The level of this resource
        /// </summary>
        public ResourceLevel Level { get; private set; }

        /// <summary>
        /// A collection of child resources that belong to this resource
        /// </summary>
        public IList<Resource> Children { get; private set; }

        /// <summary>
        /// The routes that have been configured for this resource
        /// </summary>
        public IList<Route> Routes { get; private set; }

        /// <summary>
        /// The parent of this resource
        /// </summary>
        public Resource Parent { get; private set; }

        /// <summary>
        /// Gets all ancestors, starting from this resource's immediate parent
        /// </summary>
        public IEnumerable<Resource> Ancestors
        {
            get
            {
                if (Parent != null 
                    && Parent.Level != ResourceLevel.Base)
                {
                    yield return Parent;
                    foreach (var ancestor in Parent.Ancestors)
                    {
                        yield return ancestor;
                    }
                }
            }
        }

        /// <summary>
        /// Additional data assigned to this Resource - designed for use by application-specific
        /// extensions to core routing functionality.
        /// </summary>
        public IDictionary<string, object> CustomProperties { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("FullName: {0}, Level: {1}", FullName, Level);
        }
    }
}