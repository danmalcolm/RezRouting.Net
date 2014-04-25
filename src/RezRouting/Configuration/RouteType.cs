using System.Collections.Generic;
using System.Web.Routing;
using RezRouting.Utility;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Configuration for an individual route that applies to an action on a resource
    /// </summary>
    public class RouteType
    {
        public RouteType(string name, IEnumerable<ResourceType> resourceTypes, CollectionLevel collectionLevel, string controllerAction, string urlPath, string httpMethod, int mappingOrder, object requestValues = null)
        {
            Name = name;
            ResourceTypes = resourceTypes.ToReadOnlyList();
            CollectionLevel = collectionLevel;
            ControllerAction = controllerAction;
            HttpMethod = httpMethod;
            UrlPath = urlPath;
            MappingOrder = mappingOrder;
            QueryStringValues = new RouteValueDictionary(requestValues ?? new object());
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
        public string ControllerAction { get; private set; }
        
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
                        "Name: {0}, ResourceTypes: {1}, CollectionLevel: {2}, ControllerAction: {3}, HttpMethod: {4}, UrlPath: {5}, MappingOrder: {6}, QueryStringValues: {7}",
                        Name, string.Join(",", ResourceTypes), CollectionLevel, ControllerAction, HttpMethod, UrlPath,
                        MappingOrder, QueryStringValues);
            }
        }
    }
}