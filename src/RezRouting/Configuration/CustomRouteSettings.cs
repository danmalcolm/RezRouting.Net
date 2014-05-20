using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Additional customizations specified at RouteType level for a specific resource
    /// </summary>
    internal class CustomRouteSettings
    {
        public CustomRouteSettings(RouteValueDictionary queryStringValues, bool include, string pathSegment)
        {
            QueryStringValues = queryStringValues;
            Include = include;
            PathSegment = pathSegment;
        }

        public RouteValueDictionary QueryStringValues { get; private set; }
        
        public bool Include { get; private set; }

        public string PathSegment { get; set; }
    }
}