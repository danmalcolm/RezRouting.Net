using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Mutable representation of resource used during resource configuration by builders 
    /// and extensions
    /// </summary>
    public abstract class ResourceData
    {
        private List<ResourceData> children = new List<ResourceData>();
        private List<RouteData> routes = new List<RouteData>();

        public ResourceData()
        {
            CustomProperties = new CustomValueCollection();
            ExtensionData = new CustomValueCollection();
        }

        internal void Init(string name, ResourceData parent)
        {
            Parent = parent;
            Name = name;
            if (parent != null)
            {
                Parent.AddChild(this);
            }
        }

        private void AddChild(ResourceData child)
        {
            children.Add(child);
        }

        public ResourceData Parent { get; private set; }

        public bool IsRoot
        {
            get { return Parent == null; }
        }

        /// <summary>
        /// The full unique name of the resource based on the names of the resource
        /// and its ancestors
        /// </summary>
        public string FullName 
        {
            get
            {
                string parentFullName = Parent != null ? Parent.FullName : "";
                return string.Concat(parentFullName, string.IsNullOrWhiteSpace(parentFullName) ? "" : ".", Name);
            } 
        }

        /// <summary>
        /// The name of the resource 
        /// </summary>
        public string Name { get; private set; }

        public abstract ResourceType Type { get; }

        public IEnumerable<ResourceData> Children
        {
            get { return children.AsEnumerable(); }
        }

        public IEnumerable<RouteData> Routes
        {
            get { return routes.AsEnumerable(); }
        }

        public CustomValueCollection CustomProperties { get; private set; }

        public CustomValueCollection ExtensionData { get; private set; }

        public IdUrlSegment AncestorIdUrlSegment { get; set; }

        /// <summary>
        /// Gets a "flattened" resource hierarchy, returning all resources in the 
        /// specified sequence and their descendants
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        private static IEnumerable<ResourceData> Expand(IEnumerable<ResourceData> resources)
        {
            return resources.SelectMany(resource => new[] { resource }.Concat(Expand(resource.Children)));
        }

        /// <summary>
        /// Gets a "flattened" list of resources, returning the current resource 
        /// and its descendants
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResourceData> Expand()
        {
            return Expand(new[] { this });
        }

        public void AddRoute(RouteData route)
        {
            if (route == null) throw new ArgumentNullException("route");
            routes.Add(route);
        }

        /// <summary>
        /// Gets URL segment within the resource's route URL template
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract IUrlSegment GetUrlSegment(ConfigurationOptions options);
        
        public Resource CreateResource(ConfigurationOptions options)
        {
            var childResources = this.children.Select(x => x.CreateResource(options));
            var routes = this.routes.Select(x => x.CreateRoute());
            var urlSegment = GetUrlSegment(options);
            return new Resource(Name, urlSegment, Type, AncestorIdUrlSegment, CustomProperties, routes, childResources);
        }

        /// <summary>
        /// Creates a duplicate of the current ResourceData object, creating deep 
        /// copies of all descendant resources and routes within the resource hierarchy
        /// </summary>
        /// <returns></returns>
        public ResourceData Copy(ResourceData parent)
        {
            var copy = CreateCopy();
            copy.Init(Name, parent);
            children.Each(c => c.Copy(copy)); // They will be added to parent
            copy.routes = routes.Select(x => x.Copy()).ToList();
            copy.CustomProperties = new CustomValueCollection(CustomProperties);
            copy.ExtensionData = new CustomValueCollection(ExtensionData);
            copy.AncestorIdUrlSegment = AncestorIdUrlSegment;
            return copy;
        }

        protected abstract ResourceData CreateCopy();
    }
}