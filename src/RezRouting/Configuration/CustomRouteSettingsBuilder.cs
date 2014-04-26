using System;
using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Enables customization of an individual Route's properties
    /// </summary>
    public class CustomRouteSettingsBuilder
    {
        private string nameSuffix = "";
        private RouteValueDictionary queryStringValues = new RouteValueDictionary();

        public CustomRouteSettingsBuilder(Type controllerType, int matchingControllerIndex)
        {
            ControllerType = controllerType;
            MatchingControllerIndex = matchingControllerIndex;
        }

        /// <summary>
        /// The type of the controller for the current route
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// The index of the controller among those containing actions that
        /// match the current route.
        /// </summary>
        public int MatchingControllerIndex { get; set; }

        /// <summary>
        /// Sets a value that will be added to the name of the Route
        /// </summary>
        /// <param name="suffix"></param>
        public void NameSuffix(string suffix)
        {
            if (suffix == null) throw new ArgumentNullException("suffix");
            nameSuffix = suffix ?? "";
        }

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
            return new CustomRouteSettings(nameSuffix, queryStringValues);
        }
    }
}