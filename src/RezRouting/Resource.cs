﻿using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting
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

        // TODO: sort chicken-and-egg - does Route need resource, or just some of its props
        internal void InitRoutes(IEnumerable<Route> routes)
        {
            Routes = routes.ToReadOnlyList();
            Routes.Each(route => route.InitResource(this));
        }

        private void InitParent(Resource parent)
        {
            Parent = parent;
        }

        public string Name { get; private set; }

        public string FullName
        {
            get {
                // TODO - optimise (create once on construction?)
                string parentFullName = Parent != null ? Parent.FullName : "";
                return string.Concat(parentFullName, string.IsNullOrWhiteSpace(parentFullName) ? "" : ".", Name);

            }
        }

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

        public ResourceLevel Level { get; private set; }

        public IList<Resource> Children { get; private set; }

        public IList<Route> Routes { get; private set; }

        public Resource Parent { get; private set; }
    }
}