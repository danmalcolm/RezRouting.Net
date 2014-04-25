using System.Web;
using System.Web.Routing;

namespace RezRouting.Routing
{
    public class QueryStringValueConstraint : IRouteConstraint
    {
        private readonly string key;
        private readonly string value;

        public QueryStringValueConstraint(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest)
            {
                return Equals(httpContext.Request.QueryString[key], value);
            }
            else
            {
                return Equals(values[key], value);
            }
        }
    }
}
         
    
