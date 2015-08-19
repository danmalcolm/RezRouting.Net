using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Implementation of HttpMethodConstraint that allows one of the following to specify
    /// the intended HTTP method in a POST request:
    /// 
    /// X-HTTP-Method-Override value in form data
    /// _method value in form data
    /// X-HTTP-Method-Override value in HTTP request header
    /// 
    /// This is used to support browsers where PUT and DELETE cannot be used
    /// </summary>
    public class HttpMethodOrOverrideConstraint : HttpMethodConstraint
    {
        private static readonly string[] FormOverrideKeys = { "X-HTTP-Method-Override", "_method" };
        private static readonly string[] HeaderOverrideKeys = { "X-HTTP-Method-Override" };

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
                var form = GetUnvalidatedForm(request);
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

        /// <summary>
        /// Gets unvalidated form, allowing routing to happen without triggering
        /// validation - http://msdn.microsoft.com/en-us/library/system.web.httprequest.unvalidated(v=vs.110).aspx
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private NameValueCollection GetUnvalidatedForm(HttpRequestBase request)
        {
            if (HttpContext.Current != null)
            {
                // HACK: The Unvalidated method in System.Web.Helpers actually
                // gets values from HttpContext.Current.Request, ignoring the request
                // parameter. So we can safely use this to differentiate between
                // web app runtime and unit test
                return System.Web.Helpers.Validation.Unvalidated(request).Form;
            }
            else
            {
                return request.Form;
            }
        }

        private static string GetOverride(NameValueCollection form, string[] keys)
        {
            return keys.Select(key => form[key])
                .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        }
    }
}