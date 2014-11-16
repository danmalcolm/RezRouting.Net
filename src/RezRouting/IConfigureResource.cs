using System;
using System.Collections.Generic;

namespace RezRouting
{
    /// <summary>
    /// Configures an individual resource 
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

        void Route(string name, Type controllerType, string action, string httpMethod, string path, IDictionary<string,object> customProperties = null);
    }

    
}