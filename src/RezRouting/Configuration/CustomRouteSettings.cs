using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Additional customizations specified at RouteType level
    /// </summary>
    internal class CustomRouteSettings
    {
        public CustomRouteSettings(RouteValueDictionary queryStringValues, bool ignore)
        {
            QueryStringValues = queryStringValues;
            Ignore = ignore;
        }

        public RouteValueDictionary QueryStringValues { get; private set; }
        
        public bool Ignore { get; private set; }
    }
}