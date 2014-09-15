using System;
using RezRouting2.Options;

namespace RezRouting2
{
    /// <summary>
    /// Creates a route based on a resource and handler type during route mapping. One or more 
    /// IRouteTypes are registered with a RouteMapper during route building. Each RouteType
    /// is given the option of mapping a route for each resource and handler / controller.
    /// </summary>
    public interface IRouteType
    {
        /// <summary>
        /// Creates a route for the current resource based on the specified handler type
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="handlerType"></param>
        /// <param name="pathFormatter"></param>
        /// <returns></returns>
        Route BuildRoute(Resource resource, Type handlerType, UrlPathFormatter pathFormatter);
    }
}