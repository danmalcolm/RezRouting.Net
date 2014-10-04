using System.Collections.Generic;

namespace RezRouting
{
    /// <summary>
    /// Sets up a route
    /// </summary>
    public interface IConfigureRoute
    {
        void Configure(string name, string action, string httpMethod, string path, IDictionary<string, object> customProperties = null);
    }
}