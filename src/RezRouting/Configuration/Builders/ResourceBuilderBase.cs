using System;
using System.Collections.Generic;
using System.Linq;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Base class with shared functionality for builders used to configure each type of resource
    /// </summary>
    public abstract class ResourceBuilderBase<TData> : IResourceConfigurator, IResourceBuilder
        where TData : ResourceData, new()
    {
        /// <summary>
        /// Creates a ResourceBuilderBase
        /// </summary>
        /// <param name="name"></param>
        protected ResourceBuilderBase(ResourceData parentData, string name)
        {
            Data = new TData();
            Data.Init(name, parentData);
            ChildBuilders = new List<IResourceBuilder>();
        }

        /// <summary>
        /// The data for the resource being configured by this builder
        /// </summary>
        protected TData Data { get; private set; }

        /// <summary>
        /// The name of the Resource
        /// </summary>
        protected string Name
        {
            get { return Data.Name; }
        }

        /// <summary>
        /// A collection of builders used to configure child resources
        /// </summary>
        private List<IResourceBuilder> ChildBuilders { get; set; }

        /// <summary>
        /// Adds a child resource to the current resource being configured
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="childBuilder"></param>
        /// <param name="configure"></param>
        protected void AddChild<T>(T childBuilder, Action<T> configure)
            where T : IResourceBuilder
        {
            configure(childBuilder);
            ChildBuilders.Add(childBuilder);
        }

        /// <inheritdoc />
        public void ExtensionData(Action<CustomValueCollection> configure)
        {
            if (configure == null) throw new ArgumentNullException("configure");

            configure(Data.ExtensionData);
        }

        /// <inheritdoc />
        public void Singular(string name, Action<ISingularConfigurator> configure)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (configure == null) throw new ArgumentNullException("configure");

            AddChild(new SingularBuilder(Data, name), x => configure(x));
        }

        /// <inheritdoc />
        public void Collection(string name, Action<ICollectionConfigurator> configure)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (configure == null) throw new ArgumentNullException("configure");

            AddChild(new CollectionBuilder(Data, name), x => configure(x));
        }

        /// <inheritdoc />
        public void Collection(string name, string itemName, Action<ICollectionConfigurator> configure)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (itemName == null) throw new ArgumentNullException("itemName");

            AddChild(new CollectionBuilder(Data, name, itemName), x => configure(x));
        }

        /// <inheritdoc />
        public void AncestorIdName(string name)
        {
            Data.AncestorIdUrlSegment = new IdUrlSegment(name);
        }

        /// <inheritdoc />
        public void CustomProperties(Action<CustomValueCollection> configure)
        {
            if (configure == null) throw new ArgumentNullException("configure");

            configure(Data.CustomProperties);
        }

        /// <inheritdoc />
        public void Route(string name, string httpMethod, string path, IResourceRouteHandler handler, CustomValueCollection customValues = null, CustomValueCollection additionalRouteValues = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (handler == null) throw new ArgumentNullException("handler");
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (path == null) throw new ArgumentNullException("path");

            var route = new RouteData(name, httpMethod, path, handler, customProperties: customValues, additionalRouteValues: additionalRouteValues);
            Data.AddRoute(route);
        }

        /// <inheritdoc />
        public Resource Build(ConfigurationOptions options, ConfigurationContext context)
        {
            throw new NotImplementedException();
//            if (options == null) throw new ArgumentNullException("options");
//            var children = ChildBuilders.Select(x => x.Build(options, context)).ToList();
//            var resource = Data.CreateResource(options, children);
//            var sharedConventionData = context.SharedExtensionData;
//            
//            var conventionRoutes = from convention in context.RouteConventions
//                                   from route in convention.Create(Data, sharedConventionData, Data.ExtensionData, options.UrlPathSettings, context.Cache)
//                                   select route;
//
//            var allRoutes = Data.Routes.Concat(conventionRoutes);
//            resource.InitRoutes(allRoutes);
//
//            return resource;
        }
    }
}