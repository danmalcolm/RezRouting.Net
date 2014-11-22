using System;
using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures a Resource model, allowing routes, child resources and other
    /// options to be specified
    /// </summary>
    public interface IConfigureResource
    {
        /// <summary>
        /// Adds a singular resource as a child of the current resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configure"></param>
        void Singular(string name, Action<IConfigureSingular> configure);

        /// <summary>
        /// Adds a collection resource as a child of the current resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configure"></param>
        void Collection(string name, Action<IConfigureCollection> configure);

        /// <summary>
        /// Adds a collection resource as a child of the current resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="itemName"></param>
        /// <param name="configure"></param>
        void Collection(string name, string itemName, Action<IConfigureCollection> configure);

        /// <summary>
        /// Adds a type of controller or handler responsible for handling routes belonging
        /// to the current resource. More than one type can be added by calling the method
        /// multiple times.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void HandledBy<T>();

        /// Adds a type of controller or handler responsible for handling routes belonging
        /// to the current resource. More than one type can be added by calling the method
        /// multiple times.
        void HandledBy(Type type);

        /// <summary>
        /// Adds a route to the current resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="controllerType"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <param name="path"></param>
        /// <param name="customProperties"></param>
        void Route(string name, Type controllerType, string action, string httpMethod, string path, IDictionary<string,object> customProperties = null);

        /// <summary>
        /// Sets custom properties stored on the resource being configured - intended for use by application-specific functionality and extensions)
        /// </summary>
        /// <param name="properties"></param>
        void CustomProperties(IDictionary<string, object> properties);
    }
}