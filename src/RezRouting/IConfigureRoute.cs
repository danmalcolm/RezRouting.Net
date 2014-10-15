using System.Collections.Generic;

namespace RezRouting
{
    /// <summary>
    /// Sets up a route
    /// </summary>
    public interface IConfigureRoute
    {
        /// <summary>
        /// Sets up a route based on the specified options
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="httpMethod"></param>
        /// <param name="path"></param>
        /// <param name="customProperties"></param>
        void Configure(string name, string action, string httpMethod, string path, IDictionary<string, object> customProperties = null);
    }
}