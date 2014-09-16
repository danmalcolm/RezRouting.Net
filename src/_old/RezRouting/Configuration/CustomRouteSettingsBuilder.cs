﻿using System;
using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Applies changes to an individual Route's properties
    /// </summary>
    public class CustomRouteSettingsBuilder
    {
        private RouteValueDictionary queryStringValues = new RouteValueDictionary();

        public CustomRouteSettingsBuilder(ResourceType resourceType, ResourceName resourceName, Type controllerType)
        {
            ResourceType = resourceType;
            ResourceName = resourceName;
            ControllerType = controllerType;
            PathSegment = "";
            Include = true;
            IncludeControllerInRouteName = false;
        }

        /// <summary>
        /// The type of resource for which the route is being created
        /// </summary>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// The name of the resource
        /// </summary>
        public ResourceName ResourceName { get; set; }

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
            return new CustomRouteSettings(queryStringValues, Include, PathSegment, IncludeControllerInRouteName, CollectionLevel);
        }

        /// <summary>
        /// Specifies whether the route will be mapped or ignored
        /// </summary>
        /// <returns></returns>
        public bool Include { get; set; }

        /// <summary>
        /// Specifies whether the controller name should be included in the name of
        /// the route mapped for the controller type
        /// </summary>
        public bool IncludeControllerInRouteName { get; set; }

        /// <summary>
        /// The level at which the route will be mapped for a collection resource
        /// </summary>
        public CollectionLevel CollectionLevel { get; set; }
    }
}