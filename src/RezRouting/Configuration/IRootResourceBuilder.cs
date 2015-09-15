using System;
using RezRouting.Configuration.Extensions;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures routes and other attributes of the root resource and its descendants.
    /// </summary>
    public interface IRootResourceBuilder : ISingularConfigurator
    {
        /// <summary>
        /// Adds one or more extensions used to customise the root resource and it's
        /// hierarchy
        /// </summary>
        /// <param name="extensions"></param>
        void Extension(params IExtension[] extensions);

        /// <summary>
        /// Modifies shared custom data made available to extensions when creating this resource and its routes.
        /// This data defined at root level and shared by all resources in the hierarchy.
        /// </summary>
        /// <param name="configure">An action that modifies the shared collection of extension data</param>
        void SharedExtensionData(Action<CustomValueCollection> configure);
        
        /// <summary>
        /// Configures options applied during resource and route configuration
        /// </summary>
        /// <param name="configure">An action that configures the options</param>
        void Options(Action<IOptionsConfigurator> configure);

        /// <summary>
        /// Creates a model object representing the root resource and any descendants 
        /// that have been configured. 
        /// </summary>
        /// <returns></returns>
        Resource Build();
    }
}