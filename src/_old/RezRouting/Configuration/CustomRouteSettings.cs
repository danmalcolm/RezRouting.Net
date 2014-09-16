using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Additional customizations specified at RouteType level for a specific resource
    /// </summary>
    public class CustomRouteSettings
    {
        public CustomRouteSettings(RouteValueDictionary queryStringValues, bool include, string pathSegment, bool includeControllerInRouteName, CollectionLevel collectionLevel)
        {
            CollectionLevel = collectionLevel;
            QueryStringValues = queryStringValues;
            Include = include;
            PathSegment = pathSegment;
            IncludeControllerInRouteName = includeControllerInRouteName;
        }

        public RouteValueDictionary QueryStringValues { get; private set; }
        
        public bool Include { get; private set; }

        public string PathSegment { get; set; }

        public bool IncludeControllerInRouteName { get; set; }

        public CollectionLevel CollectionLevel { get; private set; }
    }
}