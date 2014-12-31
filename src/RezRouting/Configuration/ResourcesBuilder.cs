using System;
using System.Collections.Generic;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// RezRouting's main entry-point for resource configuration. Configures resource hierarchy 
    /// and routes for an application (or part of an application). 
    /// </summary>
    public class ResourcesBuilder : IConfigureChildren
    {
        private readonly RootBuilder rootBuilder = new RootBuilder();
        private readonly List<IRouteConvention> routeConventions = new List<IRouteConvention>();
        private readonly OptionsBuilder optionsBuilder = new OptionsBuilder();

        /// <summary>
        /// Configures a collection-level Resource
        /// </summary>
        /// <param name="name">The name of the collection, used in route name and (by default)
        /// in URLs.</param>
        /// <param name="configure">Action used to perform further configuration of the resource and its routes</param>
        public void Collection(string name, Action<IConfigureCollection> configure)
        {
            rootBuilder.Collection(name, configure);
        }
        
        /// <summary>
        /// Configures a singular resource
        /// </summary>
        /// <param name="name">The name of the resource, used in route name and (by default)
        /// in URLs.</param>
        /// <param name="configure">Action used to perform further configuration of the resource and its routes</param>
        public void Singular(string name, Action<IConfigureSingular> configure)
        {
            rootBuilder.Singular(name, configure);
        }

        /// <summary>
        /// Adds one or more route conventions from a route convention scheme that will be 
        /// used to generate routes for all resources configured by this ResourcesBuilder
        /// </summary>
        /// <param name="scheme"></param>
        public void IncludeRouteConventions(IRouteConventionScheme scheme)
        {
            var conventions = scheme.GetConventions();
            this.routeConventions.AddRange(conventions);
        }

        /// <summary>
        /// Sets options that control the way in which routes are configured by this
        /// ResourcesBuilder
        /// </summary>
        /// <param name="configure"></param>
        public void Options(Action<IConfigureOptions> configure)
        {
            configure(optionsBuilder);
        }

        /// <summary>
        /// Sets a base path for resource route URLs. All resource route URLs will be nested 
        /// below the specified path. 
        /// </summary>
        /// <param name="path"></param>
        public void BasePath(string path)
        {
            rootBuilder.UrlPath(path);
        }

        /// <summary>
        /// Sets a base name for resources. The specified name will be included in the
        /// full names of all resources and routes mapped by this ResourcesBuilder. If more
        /// than one set of resource routes are being configured, it is recommended
        /// that a unique base name is used for each to prevent naming clashes.
        /// </summary>
        /// <param name="name"></param>
        public void BaseName(string name)
        {
            rootBuilder.ChangeName(name);
        }
        
        /// <summary>
        /// Creates a ResourcesModel
        /// </summary>
        /// <returns></returns>
        public ResourcesModel Build()
        {
            var options = optionsBuilder.Build();
            var context = new RouteMappingContext(routeConventions, options);
            var rootResource = rootBuilder.Build(context);
            return new ResourcesModel(rootResource.Children);
        }
    }
}