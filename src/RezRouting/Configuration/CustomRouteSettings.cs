using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Additional customizations specified at RouteType level for a specific resource
    /// </summary>
    internal class CustomRouteSettings
    {
        public CustomRouteSettings(RouteValueDictionary queryStringValues, bool ignore, string pathSegment)
        {
            QueryStringValues = queryStringValues;
            Ignore = ignore;
            PathSegment = pathSegment;
        }

        public RouteValueDictionary QueryStringValues { get; private set; }
        
        public bool Ignore { get; private set; }

        public string PathSegment { get; set; }
    }
}