using System.Collections.Generic;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration.Conventions
{
    /// <summary>
    /// Creates Routes for a Resource object during Resource and Route configuration. 
    /// IRouteConvention are expected to inspect each resource and create any Routes
    /// that apply to the resource, allowing common types of route to be set up 
    /// according to shared conventions. An example of logic handled by a convention is
    /// "create an index route for all Collection resources that have a controller 
    /// with an Index action method."
    /// </summary>
    public interface IRouteConvention
    {
        /// <summary>
        /// Creates routes that apply to a resource based on the supplied handlers
        /// </summary>
        /// <param name="resource">The resource object</param>
        /// <param name="data">Data added during resource configuration to drive conventions</param>
        /// <param name="urlPathSettings"></param>
        /// <param name="contextItems"></param>
        /// <returns></returns>
        IEnumerable<Route> Create(Resource resource, CustomValueCollection data, UrlPathSettings urlPathSettings, CustomValueCollection contextItems);
    }
}