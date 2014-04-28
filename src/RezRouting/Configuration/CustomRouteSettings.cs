using System.Web.Routing;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Additional customizations specified at RouteType level
    /// </summary>
    internal class CustomRouteSettings
    {
        public CustomRouteSettings(RouteValueDictionary queryStringValues)
        {
            QueryStringValues = queryStringValues;
        }

        public RouteValueDictionary QueryStringValues { get; private set; }
    }
}