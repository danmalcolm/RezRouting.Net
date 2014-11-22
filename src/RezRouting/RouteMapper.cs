using System;
using System.Collections.Generic;
using RezRouting.Options;

namespace RezRouting
{
    /// <summary>
    /// Entry point used to set up resource routes for an application, or part
    /// of an application
    /// </summary>
    public class RouteMapper
    {
        private readonly BaseBuilder baseBuilder = new BaseBuilder();
        private readonly List<IRouteConvention> routeConventions = new List<IRouteConvention>();
        private readonly OptionsBuilder optionsBuilder = new OptionsBuilder();

        /// <summary>
        /// Configures a resource collection
        /// </summary>
        /// <param name="name">The name of the collection, used in route name and (by default)
        /// in URLs.</param>
        /// <param name="configure">Action used to perform further configuration of the resource and its routes</param>
        public void Collection(string name, Action<IConfigureCollection> configure)
        {
            baseBuilder.Collection(name, configure);
        }

        /// <summary>
        /// Configures a resource collection - including specification of the name of child
        /// item resources
        /// </summary>
        /// <param name="name">The name of the collection, used in route name and (by default)
        /// in URLs.</param>
        /// <param name="itemName">Specifies explicitly the name used for item resources within
        /// this collection - by default the item name is based on a singular version of the 
        /// collection name</param>
        /// <param name="configure">Action used to perform further configuration of the resource and its routes</param>
        public void Collection(string name, string itemName, Action<IConfigureCollection> configure)
        {
            baseBuilder.Collection(name, itemName, configure);
        }

        /// <summary>
        /// Configures a singular resource
        /// </summary>
        /// <param name="name">The name of the resource, used in route name and (by default)
        /// in URLs.</param>
        /// <param name="configure">Action used to perform further configuration of the resource and its routes</param>
        public void Singular(string name, Action<IConfigureSingular> configure)
        {
            baseBuilder.Singular(name, configure);
        }

        /// <summary>
        /// Sets shared conventions used to generate routes for all resources configured by 
        /// this RouteMapper
        /// </summary>
        /// <param name="conventions"></param>
        public void RouteConventions(params IRouteConvention[] conventions)
        {
            this.routeConventions.AddRange(conventions);
        }

        /// <summary>
        /// Sets shared conventions used to generate routes for all resources configured by 
        /// this RouteMapper
        /// </summary>
        /// <param name="conventions"></param>
        public void RouteConventions(IEnumerable<IRouteConvention> conventions)
        {
            this.routeConventions.AddRange(conventions);
        }

        /// <summary>
        /// Sets options used that control the way in which routes are configured by this
        /// RouteMapper
        /// </summary>
        /// <param name="configure"></param>
        public void Options(Action<IConfigureOptions> configure)
        {
            configure(optionsBuilder);
        }

        /// <summary>
        /// Sets a base path for resource URLs. All URLs will be nested below the specified
        /// path.
        /// </summary>
        /// <param name="path"></param>
        public void BasePath(string path)
        {
            baseBuilder.UrlPath(path);
        }

        /// <summary>
        /// Sets a base name for resources. The specified name will be included in the
        /// full names of all resources and routes mapped by this RouteMapper.
        /// </summary>
        /// <param name="name"></param>
        public void BaseName(string name)
        {
            baseBuilder.SetName(name);
        }
        
        /// <inheritdoc />
        public virtual ResourcesModel Build()
        {
            var options = optionsBuilder.Build();
            var context = new RouteMappingContext(routeConventions, options);
            var rootResource = baseBuilder.Build(context);
            return new ResourcesModel(rootResource.Children);
        }
    }
}