using System;
using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Defines a blueprint for a route that applies to a specific resource. RezRouting
    /// contains standard RouteTypes and can be extended with application-specific RouteTypes.
    /// </summary>
    public class RouteType
    {
        /// <summary>
        /// Creates a new instance of RouteType
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resourceTypes"></param>
        /// <param name="actionName"></param>
        /// <param name="httpMethod"></param>
        /// <param name="mappingOrder"></param>
        /// <param name="customize">A function that customize each Route that is mapped based on this RouteType. This method is called with each
        /// controller type that has an action method matching this RouteType's actionName (using the resource.HandledBy methods) .
        /// </param>
        public RouteType(string name, 
            IEnumerable<ResourceType> resourceTypes, 
            string actionName, 
            string httpMethod, 
            int mappingOrder,
            Action<CustomRouteSettingsBuilder> customize = null
        )
        {
            Name = name;
            ResourceTypes = resourceTypes.ToReadOnlyList();
            ActionName = actionName;
            HttpMethod = httpMethod;
            MappingOrder = mappingOrder;
            Customize = customize ?? (x => { });
        }
        
        /// <summary>
        /// The name of the route - used for reference and also used
        /// in the Routes that are created
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
        public int MappingOrder { get; private set; }

        /// <summary>
        /// Action that performs additional customization of a route's properties
        /// before it is mapped
        /// </summary>
        public Action<CustomRouteSettingsBuilder> Customize { get; private set; }

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
                        "Name: {0}, ResourceTypes: {1}, ActionName: {3}, HttpMethod: {4}, MappingOrder: {5}",
                        Name, string.Join(",", ResourceTypes), ActionName, HttpMethod, MappingOrder);
            }
        }
        

        internal CustomRouteSettings GetCustomSettings(ResourceType resourceType, ResourceName resourceName, Type controllerType)
        {
            var builder = new CustomRouteSettingsBuilder(resourceType, resourceName, controllerType);
            Customize(builder);
            return builder.Build();
        }
    }
}