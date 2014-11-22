using System;
using System.Collections.Generic;
using RezRouting.Configuration.Options;
using RezRouting.Resources;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Creates Routes for a Resource object during Resource and Route configuration. 
    /// IRouteConvention are expected to inspect each resource and create any Routes
    /// that apply to the resource, allowing common types of route to be set up 
    /// according to shared conventions. An example of logic handled by a convention is
    /// "create an edit route for all CollectionItem resources that have a controller 
    /// that includes an Edit action method."
    /// </summary>
    public interface IRouteConvention
    {
        /// <summary>
        /// Creates routes that apply to a resource based on the supplied controller types
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="controllerTypes"></param>
        /// <param name="pathFormatter"></param>
        /// <returns></returns>
        IEnumerable<Route> Create(Resource resource, IEnumerable<Type> controllerTypes, UrlPathFormatter pathFormatter);
    }
}