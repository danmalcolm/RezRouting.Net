using System.Web.Routing;

namespace RezRouting.Configuration
{
    internal class CustomRouteSettings
    {
        public CustomRouteSettings(string nameSuffix, RouteValueDictionary queryStringValues)
        {
            NameSuffix = nameSuffix;
            QueryStringValues = queryStringValues;
        }

        public string NameSuffix { get; private set; }

        public RouteValueDictionary QueryStringValues { get; private set; }
    }
}