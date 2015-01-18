using System;
using System.Collections.Generic;
using RezRouting.Configuration.Conventions;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Sets up attributes of a resource during resource configuration.
    /// </summary>
    public interface IResourceConfigurator
    {
        /// <summary>
        /// Adds a component that handles one or more of a resource's routes. When route conventions
        /// are in use, they will inspect a resource's handlers to determine which routes are supported. 
        /// </summary>
        /// <param name="handler"></param>
        void HandledBy(IResourceHandler handler);

        /// <summary>
        /// Adds a route to the current resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="handler"></param>
        /// <param name="httpMethod"></param>
        /// <param name="path"></param>
        /// <param name="customProperties"></param>
        void Route(string name, IRouteHandler handler, string httpMethod, string path, IDictionary<string,object> customProperties = null);

        /// <summary>
        /// Sets custom properties stored on the resource being configured - intended for use by application-specific functionality and extensions)
        /// </summary>
        /// <param name="properties"></param>
        void CustomProperties(IDictionary<string, object> properties);

        /// <summary>
        /// Adds a child singular resource to resource being configured
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configure"></param>
        void Singular(string name, Action<ISingularConfigurator> configure);

        /// <summary>
        /// Adds a child collection resource to the resource being configured
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configure"></param>
        void Collection(string name, Action<ICollectionConfigurator> configure);
    }
}
