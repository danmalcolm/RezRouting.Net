using System;
using System.Collections.Generic;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up attributes of a resource during resource configuration.
    /// </summary>
    public interface IResourceConfigurator
    {
        /// <summary>
        /// Adds a route to the current resource
        /// </summary>
        /// <param name="name">The name of the route - combined with the full name of the resource to give the full route name</param>
        /// <param name="httpMethod">The HTTP method that must be used for this route to match</param>
        /// <param name="path">The path within the URL used to identify the route - appended to the resource's URL to create the full route URL</param>
        /// <param name="handler">The handler that handles the route</param>
        /// <param name="customValues">Custom properties attached to the route - intended for use by application-specific functionality and extensions</param>
        /// <param name="additionalRouteValues"></param>
        void Route(string name, string httpMethod, string path, IResourceRouteHandler handler, CustomValueCollection customValues = null, CustomValueCollection additionalRouteValues = null);

        /// <summary>
        /// Sets custom properties stored on the resource being configured - intended for use by application-specific functionality and extensions
        /// </summary>
        /// <param name="configure">An action that modifies the collection of custom properties</param>
        void CustomProperties(Action<CustomValueCollection> configure);

        /// <summary>
        /// Modifies custom data made available to conventions when creating this resource and its routes
        /// </summary>
        /// <param name="configure">An action that modifies the collection of convention data</param>
        void ConventionData(Action<CustomValueCollection> configure);
        
        /// <summary>
        /// Adds a child singular resource to resource being configured
        /// </summary>
        /// <param name="name">The name of the child resource</param>
        /// <param name="configure">An action that configures the child resource</param>
        void Singular(string name, Action<ISingularConfigurator> configure);

        /// <summary>
        /// Adds a child collection resource to the resource being configured
        /// </summary>
        /// <param name="name">The name of the child collection resource. The name of item resources
        /// within the new collection will be based on the singular of the collection name. If the
        /// singular name cannot be determined, the item name will be based on the collection name
        /// suffixed with &quot;Item&quot;. An overload allows the item name to be specified explicitly.</param>
        /// <param name="configure">An action that configures the child resource</param>
        void Collection(string name, Action<ICollectionConfigurator> configure);

        /// <summary>
        /// Adds a child collection resource to the resource being configured
        /// </summary>
        /// <param name="name">The name of the child resource</param>
        /// <param name="itemName">The name used for item resources nested within the child collection resource</param>
        /// <param name="configure">An action that configures the child resource</param>
        void Collection(string name, string itemName, Action<ICollectionConfigurator> configure);

        /// <summary>
        /// Sets the name of the identifier parameter that is used to identify the closest
        /// ancestor collection item in the route URL for this resource. 
        /// </summary>
        /// <param name="name"></param>
        void AncestorIdName(string name);
    }
}
