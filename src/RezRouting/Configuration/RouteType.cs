using System;
using System.Collections.Generic;
using System.Web.Routing;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Defines properties of a type of route that applies to an action on a resource - 
    /// a blueprint for an instance of a Route created for a specific resource. RezRouting
    /// contains standard RouteTypes and can be extended with custom application-specific
    /// RouteTypes.
    /// </summary>
    public class RouteType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resourceTypes"></param>
        /// <param name="collectionLevel"></param>
        /// <param name="actionName"></param>
        /// <param name="urlPath"></param>
        /// <param name="httpMethod"></param>
        /// <param name="mappingOrder"></param>
        /// <param name="includeController">A function that determines whether a route should be set up 
        /// on the controller with the action specified on the RouteType. The function is called with each
        /// controller that has been set up for an individual resource (using the resource.HandledBy methods)
        /// that has an action method matching this RouteType's actionName. 2 arguments are supplied, the 
        /// controller type and an index representing its position in the sequence of matching controllers. 
        /// If includeController is not specified, only the first controller type with an action matching
        /// the RouteType's action is used.</param>
        /// <param name="customize">A function that customizes the Route's properties</param>
        public RouteType(string name, 
            IEnumerable<ResourceType> resourceTypes, 
            CollectionLevel collectionLevel, 
            string actionName, 
            string urlPath, 
            string httpMethod, 
            int mappingOrder, 
            Action<CustomRouteSettingsBuilder> customize = null
        )
        {
            Name = name;
            ResourceTypes = resourceTypes.ToReadOnlyList();
            CollectionLevel = collectionLevel;
            ActionName = actionName;
            HttpMethod = httpMethod;
            UrlPath = urlPath;
            MappingOrder = mappingOrder;
            Customize = customize ?? (x => { });
        }
        
        /// <summary>
        /// The name of the route - this is used when mapping the route
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The types of resource to which this route applies
        /// </summary>
        public IList<ResourceType> ResourceTypes { get; private set; }

        /// <summary>
        /// For collection resources, specifies whether the action applies
        /// to an item within the collection or the collection itself
        /// </summary>
        public CollectionLevel CollectionLevel { get; private set; }

        /// <summary>
        /// The name of the controller action method
        /// </summary>
        public string ActionName { get; private set; }
        
        /// <summary>
        /// The HTTP method used to access the action
        /// </summary>
        public string HttpMethod { get; private set; }
        
        /// <summary>
        /// Additional directory appended to the route URL after the path to the resource
        /// </summary>
        public string UrlPath { get; private set; }

        /// <summary>
        /// The order in which the route should be mapped - there is potential for the
        /// Url path to conflict with the id parameter in certain circumstances, e.g.
        /// GET products/1 and GET products/new
        /// </summary>
        public int MappingOrder { get; set; }

        /// <summary>
        /// Action that performs additional customization of a route's properties
        /// before it is mapped
        /// </summary>
        public Action<CustomRouteSettingsBuilder> Customize { get; set; }

        /// <summary>
        /// A collection of additional querystring values used to constrain to this
        /// route
        /// </summary>
        public RouteValueDictionary QueryStringValues { get; set; }
        
        /// <summary>
        /// Indicates whether any properties of a route conflict with this one
        /// </summary>
        /// <param name="routeType"></param>
        /// <returns></returns>
        public bool ConflictsWith(RouteType routeType)
        {
            return Name.EqualsIgnoreCase(routeType.Name);
        }

        /// <summary>
        /// Summary of object for debugging, display in exceptions etc
        /// </summary>
        public string UserSummary
        {
            get
            {
                return
                    string.Format(
                        "Name: {0}, ResourceTypes: {1}, CollectionLevel: {2}, ActionName: {3}, HttpMethod: {4}, UrlPath: {5}, MappingOrder: {6}",
                        Name, string.Join(",", ResourceTypes), CollectionLevel, ActionName, HttpMethod, UrlPath,
                        MappingOrder);
            }
        }

        internal CustomRouteSettings GetCustomSettings(Type controllerType)
        {
            var builder = new CustomRouteSettingsBuilder(controllerType);
            Customize(builder);
            return builder.Build();
        }
    }
}