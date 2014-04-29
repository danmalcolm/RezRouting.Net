using System;
using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Enables customization of an individual Route's properties
    /// </summary>
    public class CustomRouteSettingsBuilder
    {
        private bool ignore = false;
        private RouteValueDictionary queryStringValues = new RouteValueDictionary();

        public CustomRouteSettingsBuilder(Type controllerType)
        {
            ControllerType = controllerType;
        }

        /// <summary>
        /// The type of the controller for the current route
        /// </summary>
        public Type ControllerType { get; private set; }

        /// <summary>
        /// Constrains the route to requests with the specified values in the querystring
        /// </summary>
        /// <param name="values"></param>
        public void QueryStringValues(object values)
        {
            queryStringValues = new RouteValueDictionary(values ?? null);
        }

        /// <summary>
        /// Constrains the route to requests with the specified values in the querystring
        /// </summary>
        /// <param name="values"></param>
        public void QueryStringValues(RouteValueDictionary values)
        {
            if (values == null) throw new ArgumentNullException("values");
            queryStringValues = values;
        }

        internal CustomRouteSettings Build()
        {
            return new CustomRouteSettings(queryStringValues, Ignore);
        }

        /// <summary>
        /// Specifies whether the route will be mapped or ignored
        /// </summary>
        /// <returns></returns>
        public bool Ignore { get; set; }
    }
}