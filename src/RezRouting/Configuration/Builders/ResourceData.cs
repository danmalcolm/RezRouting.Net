using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Mutable representation of resource used during resource configuration by builders 
    /// and extensions
    /// </summary>
    public abstract class ResourceData
    {
        private readonly List<ResourceData> children = new List<ResourceData>();
        private readonly List<Route> routes = new List<Route>();

        public ResourceData()
        {
            CustomProperties = new CustomValueCollection();
            ConventionData = new CustomValueCollection();
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

        public IEnumerable<Route> Routes
        {
            get { return routes.AsEnumerable(); }
        }

        public CustomValueCollection CustomProperties { get; private set; }

        public CustomValueCollection ConventionData { get; private set; }

        public IdUrlSegment AncestorIdUrlSegment { get; set; }

        public void AddRoute(Route route)
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
        
        public Resource CreateResource(ConfigurationOptions options, IEnumerable<Resource> children)
        {
            var urlSegment = GetUrlSegment(options);
            return new Resource(Name, urlSegment, Type, AncestorIdUrlSegment, CustomProperties, children);
        }
    }
}