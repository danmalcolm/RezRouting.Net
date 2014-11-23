using System;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures child resources at a specific level within resource hierarchy
    /// </summary>
    public interface IConfigureChildren
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
    }
}