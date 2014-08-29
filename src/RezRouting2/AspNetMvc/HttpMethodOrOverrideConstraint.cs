using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace RezRouting2.AspNetMvc
{
    /// <summary>
    /// Implementation of HttpMethodConstraint that allows one of the following to specify
    /// the intended HTTP method in a POST request:
    /// X-HTTP-Method-Override value in form data
    /// _method value in form data
    /// X-HTTP-Method-Override value in HTTP request header
    /// 
    /// This is used to support browsers where PUT and DELETE cannot be used
    /// </summary>
    public class HttpMethodOrOverrideConstraint : HttpMethodConstraint
    {
        private static readonly string[] FormOverrideKeys = new[] { "X-HTTP-Method-Override", "_method" };
        private static readonly string[] HeaderOverrideKeys = new[] { "X-HTTP-Method-Override" };

        public HttpMethodOrOverrideConstraint(params string[] allowedMethods)
            : base(allowedMethods) { }

        protected override bool Match(HttpContextBase httpContext, System.Web.Routing.Route route,
            string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var request = httpContext.Request;
            if (routeDirection == RouteDirection.UrlGeneration)
            {
                return base.Match(httpContext, route, parameterName, values, routeDirection);
            }
            return CheckForIncomingRequest(httpContext, route, parameterName, values, routeDirection, request);
        }

        private bool CheckForIncomingRequest(HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection, HttpRequestBase request)
        {
            if (string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
            {
                // http://msdn.microsoft.com/en-us/library/system.web.httprequest.unvalidated(v=vs.110).aspx
                var form = request.Unvalidated.Form;
                string methodOverride = GetOverride(form, FormOverrideKeys)
                                        ?? GetOverride(request.Headers, HeaderOverrideKeys);
                if (methodOverride != null)
                {
                    return AllowedMethods.Any(m => string.Equals(m, methodOverride,
                        StringComparison.OrdinalIgnoreCase));
                }
            }

            return base.Match(httpContext, route, parameterName,
                values, routeDirection);
        }

        private static string GetOverride(NameValueCollection form, string[] keys)
        {
            return keys.Select(key => form[key])
                .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        }
    }
}