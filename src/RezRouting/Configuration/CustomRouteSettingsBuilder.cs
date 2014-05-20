using System;
using System.IO;
using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Applies changes to an individual Route's properties
    /// </summary>
    public class CustomRouteSettingsBuilder
    {
        private RouteValueDictionary queryStringValues = new RouteValueDictionary();

        public CustomRouteSettingsBuilder(Type controllerType)
        {
            ControllerType = controllerType;
            PathSegment = "";
            Include = true;
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

        /// <summary>
        /// Additional path appended to the route URL after the path to the resource, e.g.
        /// an edit route might append edit: "/products/123456/edit"
        /// </summary>
        public string PathSegment { get; set; }

        internal CustomRouteSettings Build()
        {
            return new CustomRouteSettings(queryStringValues, Include, PathSegment);
        }

        /// <summary>
        /// Specifies whether the route will be mapped or ignored
        /// </summary>
        /// <returns></returns>
        public bool Include { get; set; }
    }
}