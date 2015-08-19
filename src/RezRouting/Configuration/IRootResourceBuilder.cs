using System;
using RezRouting.Configuration.Conventions;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures routes and other attributes of the root resource and its descendants.
    /// </summary>
    public interface IRootResourceBuilder : ISingularConfigurator
    {
        /// <summary>
        /// Adds a set of conventions used to generate routes for the root resource 
        /// and its descendants
        /// </summary>
        /// <param name="scheme">A scheme containing the route conventions to add</param>
        void ApplyRouteConventions(IRouteConventionScheme scheme);

        /// <summary>
        /// Adds one or more conventions used to generated routes for the root resource
        /// and its descendants
        /// </summary>
        /// <param name="conventions"></param>
        void ApplyRouteConventions(params IRouteConvention[] conventions);

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