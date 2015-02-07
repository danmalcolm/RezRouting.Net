﻿using System;
using System.Collections.Generic;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Conventions;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// RezRouting's main entry point for configuring an application or component's 
    /// eesource hierarchy and routes. Configures routes and other attributes of 
    /// the root resource and its descendants.
    /// </summary>
    public class RootResourceBuilder : SingularBuilder, IRootResourceBuilder
    {
        /// <summary>
        /// Creates a new RootResourceBuilder, RezRouting's main entry point for 
        /// configuring an application or component's resource hierarchy and routes.
        /// </summary>
        /// <param name="name">An optional name to give to the root resource. The specified 
        /// name will be included in the full name of all chlid resources and their routes.
        /// </param>
        /// <returns></returns>
        public static IRootResourceBuilder Create(string name = "")
        {
            return new RootResourceBuilder(name);
        }

        private readonly List<IRouteConvention> routeConventions = new List<IRouteConvention>(); 
        private readonly OptionsBuilder optionsBuilder = new OptionsBuilder();

        private RootResourceBuilder(string name = "") : base(name)
        {
            this.UrlPath("");
        }

        /// <inheritdoc/>
        public void ApplyRouteConventions(IRouteConventionScheme scheme)
        {
            var conventions = scheme.GetConventions();
            this.routeConventions.AddRange(conventions);
        }

        /// <inheritdoc/>
        public void Options(Action<IOptionsConfigurator> configure)
        {
            configure(optionsBuilder);
        }

        /// <inheritdoc/>
        public Resource Build()
        {
            var options = optionsBuilder.Build();
            var context = new ConfigurationContext(routeConventions);
            var root = Build(options, context);
            return root;
        }
    }
}