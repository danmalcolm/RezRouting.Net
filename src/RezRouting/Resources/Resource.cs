using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Resources
{
    /// <summary>
    /// An individual resource that has been configured for an application (or part 
    /// of an application).
    /// </summary>
    public class Resource
    {
        private readonly IUrlSegment urlSegment;
        private readonly IdUrlSegment overrideAncestorItemId;

        /// <summary>
        /// Creates a Resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="urlSegment"></param>
        /// <param name="type"></param>
        /// <param name="overrideAncestorItemId"></param>
        /// <param name="customProperties"></param>
        /// <param name="children"></param>
        public Resource(string name, IUrlSegment urlSegment, ResourceType type, IdUrlSegment overrideAncestorItemId, CustomValueCollection customProperties, IEnumerable<Route> routes, IEnumerable<Resource> children)
        {
            Name = name;
            this.urlSegment = urlSegment;
            this.overrideAncestorItemId = overrideAncestorItemId;
            Type = type;
            CustomProperties = new CustomValueCollection(customProperties);
            Routes = routes.ToReadOnlyList();
            Routes.Each(route => route.InitResource(this));
            Children = children.ToReadOnlyList();
            Children.Each(child => child.InitParent(this));
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
                string parentPath = Parent != null ? Parent.GetUrlAsAncestor(overrideAncestorItemId) : "";
                string path = UrlPathHelper.JoinPaths(parentPath, urlSegment.Path);
                return path;
            }
        }

        /// <summary>
        /// The URL used to identify this resource within the URL of a route belonging
        /// to a descendant of this resource
        /// </summary>
        /// <param name="overrideItemId">An overriden id to identify the nearest collection item</param>
        public string GetUrlAsAncestor(IdUrlSegment overrideItemId)
        {
            // TODO - optimise (create once on construction?)
            IUrlSegment currentUrlSegment;
            IdUrlSegment parentOverride;
            
            if (overrideItemId != null && Type == ResourceType.CollectionItem)
            {
                // Use overrideItemId for this (item) resource (won't apply to 
                // remaining ancestors)
                currentUrlSegment = overrideItemId;
                parentOverride = null;
            }
            else
            {
                currentUrlSegment = urlSegment;
                parentOverride = overrideItemId ?? overrideAncestorItemId;
            }

            string parentPath = Parent != null ? Parent.GetUrlAsAncestor(parentOverride) : "";
            string path = UrlPathHelper.JoinPaths(parentPath, currentUrlSegment.PathAsAncestor);
            return path;
        }

        /// <summary>
        /// The type of this resource
        /// </summary>
        public ResourceType Type { get; private set; }

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
        /// Gets a sequence containing this resource's ancestors in the resource hierarchy, from the 
        /// immediate parent to the root resource
        /// </summary>
        public IEnumerable<Resource> Ancestors
        {
            get
            {
                if (Parent != null 
                    && Parent.Type != ResourceType.Base)
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
        public CustomValueCollection CustomProperties { get; private set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("FullName: {0}, type: {1}", FullName, Type);
        }
    }
}