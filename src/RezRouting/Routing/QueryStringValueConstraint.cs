using System.Web;
using System.Web.Routing;

namespace RezRouting.Routing
{
    /// <summary>
    /// Constrains route to URLs with a specific key and value in the querystring.
    /// With URL generation, the route values are tested for the specified key and value.
    /// </summary>
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
         
    
