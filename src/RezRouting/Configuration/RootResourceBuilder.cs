using System;
using System.Collections.Generic;
using RezRouting.Configuration.Builders;
using RezRouting.Configuration.Extensions;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// RezRouting's main entry point for configuring an application or component's 
    /// resource hierarchy and routes. Configures routes and other attributes of 
    /// the root resource and its descendants.
    /// </summary>
    public class RootResourceBuilder : SingularBuilder, IRootResourceBuilder
    {
        /// <summary>
        /// Creates a new RootResourceBuilder, RezRouting's main entry point for 
        /// configuring an application or component's resource hierarchy and routes.
        /// </summary>
        /// <param name="name">An optional name to give to the root resource. The specified 
        /// name will be included in the full names of routes belonging to the root resource
        /// and to the full names of all child resources and their routes.
        /// </param>
        /// <returns></returns>
        public static IRootResourceBuilder Create(string name = "")
        {
            return new RootResourceBuilder(name);
        }

        private readonly CustomValueCollection sharedConventionData = new CustomValueCollection();

        private readonly OptionsBuilder optionsBuilder = new OptionsBuilder();

        private readonly List<IExtension> extensions = new List<IExtension>();

        private RootResourceBuilder(string name = "") : base(null, name)
        {
            this.UrlPath("");
        }

        /// <inheritdoc/>
        public void Extension(params IExtension[] extensions)
        {
            if(extensions == null) throw new ArgumentNullException("extensions");
            this.extensions.AddRange(extensions);
        }

        /// <inheritdoc />
        public void SharedExtensionData(Action<CustomValueCollection> configure)
        {
            if (configure == null) throw new ArgumentNullException("configure");

            configure(sharedConventionData);
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
            var context = new ConfigurationContext(sharedConventionData);
            // Building resources should not modify the state of the manually configured
            // resource data, so extensions operate on a temporary copy of the resource
            // data
            var currentBuildData = Data.Copy(null);
            // Apply extensions
            extensions.Each(e => e.Extend(currentBuildData, context, options));
            
            var root = currentBuildData.CreateResource(options);
            return root;
        }
    }
}