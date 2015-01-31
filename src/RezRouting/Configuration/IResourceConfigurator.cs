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
        /// <param name="name">The name of the route</param>
        /// <param name="handler">The handler that handles the route</param>
        /// <param name="httpMethod">The HTTP method that request will be under for route to applie</param>
        /// <param name="path">The path within the URL used to identify the route - appended to the resource's URL to create the full route URL</param>
        /// <param name="customProperties">Custom properties attached to the route - intended for use by application-specific functionality and extensions</param>
        void Route(string name, IResourceRouteHandler handler, string httpMethod, string path, IDictionary<string,object> customProperties = null);

        /// <summary>
        /// Sets custom properties stored on the resource being configured - intended for use by application-specific functionality and extensions
        /// </summary>
        /// <param name="properties"></param>
        void CustomProperties(IDictionary<string, object> properties);

        /// <summary>
        /// Modifies custom data made available to conventions when creating this resource and its routes
        /// </summary>
        /// <param name="configure">An action that modifies the collection containing customer data</param>
        void ConventionData(Action<Dictionary<string, object>> configure);
        
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
    }
}
